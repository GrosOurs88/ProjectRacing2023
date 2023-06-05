using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform ExitPosition;

    private Vector3 velocityBeforeTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        {
            velocityBeforeTeleport = other.gameObject.transform.parent.transform.gameObject.GetComponent<Rigidbody>().velocity;

            //Teleport Avatar
            other.gameObject.transform.parent.transform.position = ExitPosition.position;
            other.gameObject.transform.parent.transform.rotation = ExitPosition.rotation;

            //Nullify velocity
            other.gameObject.transform.parent.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.transform.parent.transform.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            //Reapply velocity according to new orientation        
            other.gameObject.transform.parent.transform.gameObject.GetComponent<Rigidbody>().velocity =
                velocityBeforeTeleport.magnitude * ExitPosition.transform.forward;
        }
    }
}
