using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 보물 상자가 켜졌을 때, 맵이 클리어로 바뀜
public class SR_StartToEnemy1 : MonoBehaviour
{
    public Image black;
    Color color;
    Transform player;
    public Transform newPos;

    public int cnt = 0;

    public int clearEnemy = 0;

    public GameObject start;
    public GameObject start1;
    public GameObject enemy1NotClear;
    public GameObject clearEnemy1;
    public GameObject enemy1Tre;

    public GameObject enemyManager;

    private void Start()
    {
        color = black.GetComponent<Image>().color;
        color.a = 0;
        black.GetComponent<Image>().color = color;
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
            start.SetActive(false);
            // 플레이어가 스타트 부분에서 넘어오게 되면 enemy1의 notClear 맵이 켜진다.
            if (clearEnemy == 0)
            {
                enemyManager.SetActive(true);
                enemy1NotClear.SetActive(true);
            }
            // 하지만 treasure의 clearEnemy1이 0이 아니라면 clear맵이 켜진다
            else
            {
                clearEnemy1.SetActive(true);
                start1.SetActive(false);
                enemy1Tre.SetActive(true);
            }
            clearEnemy++;

            other.GetComponent<Transform>().position = newPos.position;
        }
        cnt++;
    }
    
    
}
