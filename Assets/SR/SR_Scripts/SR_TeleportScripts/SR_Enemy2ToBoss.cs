using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_Enemy2ToBoss : MonoBehaviour
{
    public Image black;
    Color color;
    Transform player;
    public Transform newPos;
    int _cnt;
    public GameObject bgm;
    public GameObject boss;
    public GameObject _boss;
    public GameObject enemy2;
    public GameObject bossHP;

    public GameObject enemy2Tre;


    public int cnt = 0;

    private void Start()
    {
        color = black.GetComponent<Image>().color;
        color.a = 0;
        black.GetComponent<Image>().color = color;
        _cnt = bgm.GetComponent<SR_BackgroundMusic>().cnt;
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
            boss.SetActive(true);
            _boss.SetActive(true);
            enemy2.SetActive(false);
            enemy2Tre.SetActive(false);
            bossHP.SetActive(true);
            other.GetComponent<Transform>().position = newPos.position;
            _cnt = 2;
            bgm.GetComponent<SR_BackgroundMusic>().cnt = _cnt;
        }
        cnt++;
    }

}
