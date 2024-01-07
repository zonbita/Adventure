using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSFX : MonoBehaviour
{
    private AudioSource source;
    public float minpitch = 0.8f;
    public float maxpitch = 2.3f;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(minpitch, maxpitch);
        source.Play();
    }
}
