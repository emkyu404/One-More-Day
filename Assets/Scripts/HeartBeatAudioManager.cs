using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class HeartBeatAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public bool active
    {
        get => !audioSource.mute;
    }
    private float lerpSpeed;
    private float maxVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;
    }
    public void ActivateHeartBeat()
    {
        audioSource.mute = false;
    }

    public void DesactivateHeartBeat()
    {
        audioSource.mute = true;
    }

    
}
