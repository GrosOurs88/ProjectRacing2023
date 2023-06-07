using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSoundWhenTouched : MonoBehaviour
{
    private AudioManager audioManager;

    private AudioSource source;
    public AudioClip sound;

    private void Start()
    {
        source = AudioManager.Instance.audioSfxSource;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !source.isPlaying)
        {
            source.PlayOneShot(sound);
        }
    }
}
