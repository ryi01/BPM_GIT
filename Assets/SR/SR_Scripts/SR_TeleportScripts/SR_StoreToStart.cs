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
            //startCanvas.SetActive(true);
            //storeCanvas.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut());

            if (clear == 0)
            {
                // 클리어되지 않은 맵이 켜지고
                store.SetActive(false);
                // 스타트 부분도 클리어 되지 않음
                start.SetActive(true);

                start.GetComponentInChildren<SR_PlayerRotate>().y = 180;
            }
            else
            {
                // 클리어된 맵이 켜짐
                storeClear.SetActive(false);
                enemy1Clear.SetActive(true);

                enemy1Clear.GetComponentInChildren<SR_PlayerRotate>().y = 85;
            }

            other.GetComponent<Transform>().position = newPos.position;
            cnt=0;
            bgm.GetComponent<SR_BackgroundMusic>().cnt = cnt;
        }
    }

}
