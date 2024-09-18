﻿using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public string mixerGroup = "";

    [Range(0f,1f)]
    public float volume;
    //[Range(0.1f, 3.0f)]
    //public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
