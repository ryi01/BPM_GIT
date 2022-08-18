using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BackgroundMusic : MonoBehaviour
{
    public int cnt = 0;

    public AudioClip[] bgm;

    new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (cnt == 0)
        {
            if (audio.clip != bgm[0])
            {
                //audio.Stop();
                audio.clip = bgm[0];
                audio.Play();
            }
        }
        else if (cnt == 1)
        {
            if (audio.clip != bgm[1])
            {
                //audio.Stop();

                audio.clip = bgm[1];
                audio.Play();
            }
        }
        else
        {
            if (audio.clip != bgm[2])
            {


                audio.clip = bgm[2];
                audio.Play(335);
            }
        }
        //audio.Stop();
        if (audio.isPlaying) print("Playing");
        print(audio.clip.name);
    }
}
