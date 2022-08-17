using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_StoreToStart : MonoBehaviour
{
    public Image black;
    Color color;
    Transform player;
    public Transform newPos;
    int cnt;
    public GameObject bgm;

    private void Start()
    {
        color = black.GetComponent<Image>().color;
        color.a = 0;
        black.GetComponent<Image>().color = color;
        cnt = bgm.GetComponent<SR_BackgroundMusic>().cnt;
    }


    IEnumerator FadeIn()
    {
        float fadeCount = 0;
        while(fadeCount<1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            black.color = new Color(0, 0, 0, fadeCount);
        }
    }
    IEnumerator FadeOut()
    {
        float fadeCount = 1.0f;
        while (fadeCount >= 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            black.color = new Color(0, 0, 0, fadeCount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //player = GameObject.Find("Player").transform;
        //StartCoroutine(FadeIn());
        //StartCoroutine(FadeOut());
        //player.GetComponent<Transform>().position = newPos.position;

        if(other.name.Contains("Player"))
        {
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut());
            other.GetComponent<Transform>().position = newPos.position;
        }
        cnt=0;
        bgm.GetComponent<SR_BackgroundMusic>().cnt = cnt;
    }
    
}
