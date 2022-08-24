using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_Enemy2ToEnemy1 : MonoBehaviour
{
    public Image black;
    Color color;
    Transform player;
    public Transform newPos;

    public GameObject enemy2;
    public GameObject enemy1;
    public GameObject enemy1Clear;

    public GameObject enemy1Tre;
    public GameObject enemy2Tre;

    public int clearEnemy2;
    public GameObject storeIcon;

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
            enemy2.SetActive(false);
            if (clearEnemy2 == 0)
            {
                enemy1.SetActive(true);
            }
            else
            {
                enemy1Clear.SetActive(false);
            }
            enemy1Tre.SetActive(true);
            enemy2Tre.SetActive(false);
            other.GetComponent<Transform>().position = newPos.position;
            clearEnemy2++;
        }
    }
    
}
