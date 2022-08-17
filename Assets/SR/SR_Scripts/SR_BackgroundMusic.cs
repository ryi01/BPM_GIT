using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BackgroundMusic : MonoBehaviour
{
    public int cnt = 0;

    public AudioClip[] bgm;

    AudioSource audio;

    private void Start()
    {
        audio.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (cnt == 0)
        {
            audio.GetComponent<AudioSource>().clip = bgm[0];
        }
        else
        {
            audio.GetComponent<AudioSource>().clip = bgm[1];
        }
            audio.Play();
    }
}
