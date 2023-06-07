using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rick : MonoBehaviour
{
    public List<GameObject> videos = new List<GameObject>();

    public float time = 60.0f;
    public float t = 0.0f;

    private void Start()
    {
        t = 0.0f;
    }

    private void Update()
    {
        t += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(t >= time)
            {
                foreach (GameObject gO in videos)
                {
                    gO.SetActive(true);
                }
            }
        }
    }
}
