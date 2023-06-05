using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManagerScript : MonoBehaviour
{
    public string RestartButtonName = "Restart";
    public string StartSceneName = "BuildStartScene";

    void Update()
    {
        if (Input.GetButton(RestartButtonName))
        {
            SceneManager.LoadSceneAsync(StartSceneName, LoadSceneMode.Single);
        }
    }
}
