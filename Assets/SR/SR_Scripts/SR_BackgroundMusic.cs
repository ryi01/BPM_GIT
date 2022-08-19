using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BackgroundMusic : MonoBehaviour
{
    public int cnt = 0;

    public AudioClip[] bgm;

    new AudioSource audio;

    GameObject boss;
    float bossHP;

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
                StartCoroutine(FadeOut());
                //audio.Stop();
                audio.clip = bgm[0];
                audio.Play();
                StartCoroutine(FadeIn());
            }
        }
        else if (cnt == 1)
        {
            if (audio.clip != bgm[1])
            {
                StartCoroutine(FadeOut());
                //audio.Stop();

                audio.clip = bgm[1];
                audio.Play();
                StartCoroutine(FadeIn());

            }
        }
        else
        {
            if (audio.clip != bgm[2])
            {
                

                StartCoroutine(FadeOut());
                audio.clip = bgm[2];
                audio.Play();
                StartCoroutine(FadeIn());

                
            }
        }

        if (cnt == 2)
        {
            boss = GameObject.Find("_Boss");
            if (boss.activeSelf == true)
            {
                bossHP = boss.GetComponent<BossHP>().enemyHP;
                if (bossHP <= 0) audio.Stop();
            }
        }

        //audio.Stop();
        //if (audio.isPlaying) print("Playing");
        //print(audio.clip.name);

    }

    IEnumerator FadeIn()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            audio.volume = fadeCount;
        }
    }
    IEnumerator FadeOut()
    {
        float fadeCount = 1.0f;
        while (fadeCount >= 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            audio.volume = fadeCount;
        }
    }
}
