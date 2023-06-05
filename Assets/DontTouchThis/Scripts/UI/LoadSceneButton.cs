using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What are the names of the scenes we want to load when clicking the button?")]
        public List<string> scenesName = new List<string>();

        private string thisSceneName;

        public void LoadTargetScene()
        {
            thisSceneName = SceneManager.GetActiveScene().name;

            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            AsyncOperation asyncLoadLevel;

            for (int i = 0; i < scenesName.Count; i++)
            {
                if (scenesName[i] != "")
                {
                    asyncLoadLevel = SceneManager.LoadSceneAsync(scenesName[i], LoadSceneMode.Additive);
                    while (!asyncLoadLevel.isDone)
                    {
                        yield return null;
                    }
                }
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
            SceneManager.UnloadSceneAsync(thisSceneName);
        }
    }
}
