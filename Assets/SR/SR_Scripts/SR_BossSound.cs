using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BossSound : MonoBehaviour
{
    float bossHP;
    public AudioClip[] endingSound;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        print(bossHP);
        bossHP = GetComponent<BossHP>().enemyHP;

        if (bossHP <= -5)
        {
            if(audio.clip != endingSound[5])
            {
                audio.Stop();
                audio.clip = endingSound[5];
                audio.Play();
            }
        }
        else if (bossHP <= -4)
        {
            if (audio.clip != endingSound[4])
            {
                audio.Stop();
                audio.clip = endingSound[4];
                audio.Play();
            }
        }
        else if (bossHP <= -3)
        {
            if (audio.clip != endingSound[3])
            {
                audio.Stop();
                audio.clip = endingSound[3];
                audio.Play();
            }
        }
        else if (bossHP <= -2)
        {
            if (audio.clip != endingSound[2])
            {
                audio.Stop();
                audio.clip = endingSound[2];
                audio.Play();
            }
        }
        else if (bossHP <= -1)
        {
            if (audio.clip != endingSound[1])
            {
                audio.Stop();
                audio.clip = endingSound[1];
                audio.Play();
            }
        }
        else if (bossHP <= 0)
        {
            if (audio.clip != endingSound[0])
            {
                audio.Stop();
                audio.clip = endingSound[0];
                audio.Play();
            }
        }


    }
}
