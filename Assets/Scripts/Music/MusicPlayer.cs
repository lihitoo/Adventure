using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource loopSource, introSource;

    void Start()
    {
        introSource.Play();
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }
}