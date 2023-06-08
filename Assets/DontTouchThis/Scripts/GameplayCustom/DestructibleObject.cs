using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public ParticleSystem particle;
    public bool onlyParticle = false;
    public bool thisIsSparta = false;
    public float spartanForce = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(particle != null)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
            }

            if(thisIsSparta)
            {
                if(gameObject.GetComponent<Rigidbody>())
                {
                    print("waaa");
                    gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * spartanForce, ForceMode.Impulse);
                }

                else
                { 
                    Debug.LogWarning("This object as no Rigidbody component!");
                }
            }

            if(!onlyParticle)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (particle != null)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
            }

            if (!onlyParticle)
            {
                Destroy(gameObject);
            }
        }
    }
}
