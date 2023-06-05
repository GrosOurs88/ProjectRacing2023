using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraScript : MonoBehaviour
{
    public enum CameraType { TPS, FPS };
    
    public bool changeCamera;
    public CameraType cameraType;
   // public float cameraSwitchTime;

    CheckPointManagerScript checkPointManagerScript;

    private void Start()
    { 
        checkPointManagerScript = CheckPointManagerScript.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            checkPointManagerScript = CheckPointManagerScript.instance;

            checkPointManagerScript.lastCheckPointPos = transform.position;
            checkPointManagerScript.lastCheckPointQuat = transform.rotation;

            if (changeCamera)
            {
                switch (cameraType)
                {
                    case CameraType.FPS:
                        checkPointManagerScript.SwitchToCameraFPS(other.transform.parent.gameObject);

                        break;
                    case CameraType.TPS:
                        checkPointManagerScript.SwitchToCameraTPS(other.transform.parent.gameObject);
                        break;
                }
            }
        }
    }    
}
