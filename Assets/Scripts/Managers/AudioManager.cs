using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
   private AudioSource audioSource;

   [Header("Audio")] 
   public AudioClip handgunFireAudio;

   public AudioClip reloadWeaponAudio;

   protected override void Awake()
   {
       base.Awake();
       audioSource = GetComponent<AudioSource>();
   }

   public void PlayAudio(AudioClip clip)
   {
       audioSource.clip = clip;
       audioSource.Play();
   }
}
