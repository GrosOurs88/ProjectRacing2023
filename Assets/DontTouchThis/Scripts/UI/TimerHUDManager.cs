using System;
using UnityEngine;
using TMPro;

public class TimerHUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    TimeManager m_TimeManager;
    
    private void Start()
    {
        m_TimeManager = FindObjectOfType<TimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<TimeManager, ObjectiveReachTargets>(m_TimeManager, this);

        if (m_TimeManager.IsFinite)
        {
            timerText.text = "";
        }
    }
    
    void Update()
    {
        if (m_TimeManager.IsFinite)
        {   
            timerText.gameObject.SetActive(true);

            int timeInt = (int)(m_TimeManager.TimeRemaining);
            int minutes = timeInt / 60;
            int seconds = timeInt % 60;
            float fraction = (m_TimeManager.TimeRemaining * 100) % 100;
            if (fraction > 99) fraction = 99;
            timerText.text = string.Format("{0}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }
    }
}
