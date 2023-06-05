using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using KartGame.KartSystems;
using UnityEngine.SceneManagement;

public enum GameState{Play, Won, Lost}

public class GameFlowManager : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("Duration of the fade-to-black at the end of the game")]
    public float endSceneLoadDelay = 3f;
    [Tooltip("The canvas group of the fade-to-black screen")]
    public CanvasGroup endGameFadeCanvasGroup;

    [Header("Win")]
    [Tooltip("This string has to be the name of the scene you want to load when winning")]
    public string winSceneName = "WinScene";
    [Tooltip("Duration of delay before the fade-to-black, if winning")]
    public float delayBeforeFadeToBlack = 4f;
    [Tooltip("Duration of delay before the win message")]
    public float delayBeforeWinMessage = 2f;
    [Tooltip("Sound played on win")]
    public AudioClip victorySound;

    [Tooltip("Prefab for the win game message")]
    public DisplayMessage winDisplayMessage;

    public PlayableDirector raceCountdownTrigger;

    [Header("Lose")]
    [Tooltip("This string has to be the name of the scene you want to load when losing")]
    public string loseSceneName = "LoseScene";
    [Tooltip("Prefab for the lose game message")]
    public DisplayMessage loseDisplayMessage;

    [HideInInspector]
    public GameState gameState { get; private set; }

    public bool autoFindKarts = true;
    public ArcadeKart playerKart;

    ArcadeKart[] karts;
    ObjectiveManager m_ObjectiveManager;
    TimeManager m_TimeManager;
    float m_TimeLoadEndGameScene;
    string m_SceneToLoad;
    float elapsedTimeBeforeEndScene = 0;

    public TimeInfos finalTime;

    public enum SpawnInSpecificZone { Nothing, Zone1_Jason, Zone2_Lionel, Zone3_Xavier, Zone4_Mathieu, Zone5_Paul, Zone6_Corentin, Zone7_Egon, Zone8_Antonio, Zone9_Louise, Zone10_Quentin, Zone11_Nicolas, Zone12_Alexandre }
    public GameObject zonesSpawnPointsList;
    public SpawnInSpecificZone zoneToSpawn;
    
    public static GameFlowManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (autoFindKarts)
        {
            karts = FindObjectsOfType<ArcadeKart>();
            if (karts.Length > 0)
            {
                if (!playerKart) playerKart = karts[0];
            }
            DebugUtility.HandleErrorIfNullFindObject<ArcadeKart, GameFlowManager>(playerKart, this);
        }

        SpawnKartInSpecificZone();

        m_ObjectiveManager = FindObjectOfType<ObjectiveManager>();
        DebugUtility.HandleErrorIfNullFindObject<ObjectiveManager, GameFlowManager>(m_ObjectiveManager, this);

        m_TimeManager = FindObjectOfType<TimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<TimeManager, GameFlowManager>(m_TimeManager, this);

        AudioUtility.SetMasterVolume(1);

        winDisplayMessage.gameObject.SetActive(false);
        loseDisplayMessage.gameObject.SetActive(false);

        m_TimeManager.StopRace();
        foreach (ArcadeKart k in karts)
        {
            k.SetCanMove(false);
        }

        //run race countdown animation
        ShowRaceCountdownAnimation();
        StartCoroutine(ShowObjectivesRoutine());

        StartCoroutine(CountdownThenStartRaceRoutine());
    }

    private void SpawnKartInSpecificZone()
    {
        switch (zoneToSpawn)
        {
            case SpawnInSpecificZone.Nothing:                
                break;
            case SpawnInSpecificZone.Zone1_Jason:
                TeleportKartToPoint(0);
                break;
            case SpawnInSpecificZone.Zone2_Lionel:
                TeleportKartToPoint(1);
                break;
            case SpawnInSpecificZone.Zone3_Xavier:
                TeleportKartToPoint(2);
                break;
            case SpawnInSpecificZone.Zone4_Mathieu:
                TeleportKartToPoint(3);
                break;
            case SpawnInSpecificZone.Zone5_Paul:
                TeleportKartToPoint(4);
                break;
            case SpawnInSpecificZone.Zone6_Corentin:
                TeleportKartToPoint(5);
                break;
            case SpawnInSpecificZone.Zone7_Egon:
                TeleportKartToPoint(6);
                break;
            case SpawnInSpecificZone.Zone8_Antonio:
                TeleportKartToPoint(7);
                break;
            case SpawnInSpecificZone.Zone9_Louise:
                TeleportKartToPoint(8);
                break;
            case SpawnInSpecificZone.Zone10_Quentin:
                TeleportKartToPoint(9);
                break;
            case SpawnInSpecificZone.Zone11_Nicolas:
                TeleportKartToPoint(10);
                break;
            case SpawnInSpecificZone.Zone12_Alexandre:
                TeleportKartToPoint(11);
                break;
        }
    }

    private void TeleportKartToPoint(int _i)
    {
        playerKart.transform.position = zonesSpawnPointsList.transform.GetChild(_i).transform.position;
        playerKart.transform.rotation = zonesSpawnPointsList.transform.GetChild(_i).transform.rotation;
    }

    IEnumerator CountdownThenStartRaceRoutine() 
    {
        yield return new WaitForSeconds(3f);
        StartRace();
    }

    void StartRace() 
    {
        foreach (ArcadeKart k in karts)
        {
			k.SetCanMove(true);
        }
        m_TimeManager.StartRace();
    }

    void ShowRaceCountdownAnimation() 
    {
        raceCountdownTrigger.Play();
    }

    IEnumerator ShowObjectivesRoutine() 
    {
        while (m_ObjectiveManager.Objectives.Count == 0)
            yield return null;
        yield return new WaitForSecondsRealtime(0.2f);
        for (int i = 0; i < m_ObjectiveManager.Objectives.Count; i++)
        {
           if (m_ObjectiveManager.Objectives[i].displayMessage)m_ObjectiveManager.Objectives[i].displayMessage.Display();
           yield return new WaitForSecondsRealtime(1f);
        }
    }

    void Update()
    {
     //   if (gameIsReady)
     //   {
            if (gameState != GameState.Play)
            {
                elapsedTimeBeforeEndScene += Time.deltaTime;
                if (elapsedTimeBeforeEndScene >= endSceneLoadDelay)
                {

                    float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
                    endGameFadeCanvasGroup.alpha = timeRatio;

                    float volumeRatio = Mathf.Abs(timeRatio);
                    float volume = Mathf.Clamp(1 - volumeRatio, 0, 1);
                    AudioUtility.SetMasterVolume(volume);

                    // See if it's time to load the end scene (after the delay)
                    if (Time.time >= m_TimeLoadEndGameScene)
                    {
                        SceneManager.LoadScene(m_SceneToLoad);
                        gameState = GameState.Play;
                    }
                }
            }
            else
            {
                if (m_ObjectiveManager.AreAllObjectivesCompleted())
                    EndGame(true);

                if (m_TimeManager.IsFinite && m_TimeManager.IsOver)
                    EndGame(false);
            }
      //  }
    }

    void EndGame(bool win)
    {
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        m_TimeManager.StopRace();

        // Remember that we need to load the appropriate end scene after a delay
        gameState = win ? GameState.Won : GameState.Lost;
        endGameFadeCanvasGroup.gameObject.SetActive(true);
        if (win)
        {
            m_SceneToLoad = winSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            finalTime.time = m_TimeManager.TimeRemaining;

            // play a sound on win
            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = victorySound;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
            audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeWinMessage);

            // create a game message
            winDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
            winDisplayMessage.gameObject.SetActive(true);
        }
        else
        {
            m_SceneToLoad = loseSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // create a game message
            loseDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
            loseDisplayMessage.gameObject.SetActive(true);
        }
    }
}
