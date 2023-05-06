using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientMusic : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public AudioClip[] musicList;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup;

        PlayNextTrack();
    }

    void PlayNextTrack()
    {
        audioSource.clip = musicList[currentTrackIndex];
        audioSource.Play();
        currentTrackIndex = (currentTrackIndex + 1) % musicList.Length;
        Invoke("PlayNextTrack", audioSource.clip.length);
    }
}