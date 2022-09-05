using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    int lastIndex = -1; // last index of music played
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private AudioClip GetRandomClip()
    {
        int index = lastIndex;
        while(index == lastIndex)
        {
            index = Random.Range(0, audioClips.Length);
        }
        lastIndex = index;
        Debug.Log("Index : " + index);
        return audioClips[index];
    }

    public void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }


}
