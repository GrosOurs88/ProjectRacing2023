using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAirplaneScript : MonoBehaviour
{
    public GameObject airPlane;
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            airPlane.GetComponent<Rigidbody>().AddForce(Vector3.right * force, ForceMode.Impulse);
            airPlane.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
