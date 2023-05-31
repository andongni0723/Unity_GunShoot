using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource fireAudioSource; 
    public AudioSource itemAudioSource;

    [Header("Audio")] 
    public AudioClip grenadeThrowAudio;
    public AudioClip grenadeBombAudio;
    public AudioClip reloadWeaponAudio;
    

    public void PlayAudio(AudioClip clip)
    {
       fireAudioSource.clip = clip;
       fireAudioSource.Play();
    }

    public void GrenadeAudio(AudioClip clip)
    {
        itemAudioSource.clip = clip;
        itemAudioSource.Play();
    }
    
}
