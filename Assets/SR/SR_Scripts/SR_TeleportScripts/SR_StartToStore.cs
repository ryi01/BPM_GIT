using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_StartToStore : MonoBehaviour
{
    public Image black;
    Color color;
    Transform player;
    public Transform newPos;
    int cnt;
    public GameObject bgm;

    public GameObject start;
    public GameObject enemy1Clear;
    public GameObject store;
    public GameObject storeClear;

    public GameObject enemy1;

    int clear;

    private void Start()
    {
        color = black.GetComponent<Image>().color;
        color.a = 0;
        black.GetComponent<Image>().color = color;
        cnt = bgm.GetComponent<SR_BackgroundMusic>().cnt;

    }

    private void Update()
    {
        clear = enemy1.GetComponent<SR_Enemy1ToStart>().clear;
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

        if(other.name == "Player")
        {
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut());
           
            if (clear == 0)
            {
                store.SetActive(true);
                start.SetActive(false);
            }
            else
            {
                storeClear.SetActive(true);
                enemy1Clear.SetActive(false);
            }
            other.GetComponent<Transform>().position = newPos.position;
            cnt=1;
            bgm.GetComponent<SR_BackgroundMusic>().cnt = cnt;
        }
    }

}
