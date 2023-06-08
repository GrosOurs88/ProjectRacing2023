using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSfxManager : MonoBehaviour
{
    static public AudioSfxManager Instance;
    public AudioSource audioSfxSource;

    public void Awake()
    {
        Instance = this;
    }

    public void EnsureSFXDestruction(AudioSource source)
    {
        StartCoroutine("DelayedSFXDestruction", source);
    }

    private IEnumerator DelayedSFXDestruction(AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return null;
        }

        GameObject.Destroy(source.gameObject);
    }
}
