using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckPointManagerScript : MonoBehaviour
{
    public static CheckPointManagerScript instance;

  //  [HideInInspector]
    public Vector3 lastCheckPointPos;
    public Quaternion lastCheckPointQuat;

    public GameObject KartClassicPlayer;

    public string teleportToLastCheckpoint = "Teleport";

    CameraManagerScript cameraManagerScript;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraManagerScript = CameraManagerScript.instance;
    }

    void Update()
    {
        if(Input.GetButton(teleportToLastCheckpoint) && lastCheckPointPos != Vector3.zero)
        {
            TeleportToLastCheckpoint();
        }
    }

    private void TeleportToLastCheckpoint()
    {
        KartClassicPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        KartClassicPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        KartClassicPlayer.transform.position = lastCheckPointPos;
        KartClassicPlayer.transform.rotation = lastCheckPointQuat;
    }

    public void SwitchToCameraFPS(GameObject _avatar)
    {
        cameraManagerScript = CameraManagerScript.instance;
        //cameraManagerScript.transform.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 1.5f;
        //cameraManagerScript.transform.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = 0;
        cameraManagerScript.animator.ResetTrigger("TPS");
        cameraManagerScript.animator.SetTrigger("FPS");


        if (_avatar.GetComponent<CustomVariableScript>())
        {
            foreach (GameObject gO in _avatar.GetComponent<CustomVariableScript>().avatarAndKart)
            {
                gO.SetActive(false);
            }
            foreach (GameObject gO in _avatar.GetComponent<CustomVariableScript>().kartWheels)
            {
                gO.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            Debug.LogError("No TemplateCharacter detected on the avatar prefab !");
        }
    }

    public void SwitchToCameraTPS(GameObject _avatar)
    {
        cameraManagerScript = CameraManagerScript.instance;
        //cameraManagerScript.transform.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 3;
        //cameraManagerScript.transform.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = -6;
        cameraManagerScript.animator.ResetTrigger("FPS");
        cameraManagerScript.animator.SetTrigger("TPS");

        if (_avatar.GetComponent<CustomVariableScript>())
        {
            foreach (GameObject gO in _avatar.GetComponent<CustomVariableScript>().avatarAndKart)
            {
                gO.SetActive(true);
            }
            foreach (GameObject gO in _avatar.GetComponent<CustomVariableScript>().kartWheels)
            {
                gO.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else
        {
            Debug.LogError("No TemplateCharacter detected on the avatar prefab !");
        }
    }
}
