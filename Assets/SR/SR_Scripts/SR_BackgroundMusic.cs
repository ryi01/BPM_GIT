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
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        print(cnt);
        if (cnt == 0)
        {
            //audio.Stop();
            audio.clip = bgm[0];
            audio.PlayOneShot(audio.clip);
        }
        else
        {
            //audio.Stop();

            audio.clip = bgm[1];
            audio.PlayOneShot(audio.clip);
        }
    }
}
