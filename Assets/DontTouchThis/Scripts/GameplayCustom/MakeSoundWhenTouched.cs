using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSoundWhenTouched : MonoBehaviour
{
    private AudioSource source;
    public AudioClip sound;

    private void Start()
    {
        source = AudioSfxManager.Instance.audioSfxSource;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !source.isPlaying)
        {
            source.PlayOneShot(sound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !source.isPlaying)
        {
            source.PlayOneShot(sound);
        }
    }
}
