using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_PlayerSound : MonoBehaviour
{
    public AudioClip[] playerSounds;
    AudioSource audio;
    float currentTime = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    
        
}
