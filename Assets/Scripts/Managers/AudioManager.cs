using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource fireAudioSource;
    public AudioSource itemAudioSource;
    public AudioSource buttonAudioSource;

    [Header("Audio")] 
    public AudioClip grenadeThrowAudio;
    public AudioClip grenadeBombAudio;
    public AudioClip scoutAudio;
    public AudioClip reloadWeaponAudio;
    public AudioClip shieldOpenAudio;
    public AudioClip shieldHitAudio;
    public AudioClip changeWeaponAudio;
    public AudioClip buttonClickAudio;


    public void PlayAudio(AudioClip clip)
    {
        fireAudioSource.clip = clip;
        fireAudioSource.Play();
    }

    public void PlayItemAudio(AudioClip clip)
    {
        itemAudioSource.clip = clip;
        itemAudioSource.Play();
    }

    public void ButtonClickAudio()
    {
        buttonAudioSource.clip = buttonClickAudio;
        buttonAudioSource.Play();
    }

}
