using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDevNameHUDScript : MonoBehaviour
{
    public string devName;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DebugDevNameDisplay.instance.DisplayName(devName);
        }
    }
}
