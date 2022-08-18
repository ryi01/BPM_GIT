using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ���ڰ� ������ ��, ���� Ŭ����� �ٲ�
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
            // �÷��̾ ��ŸƮ �κп��� �Ѿ���� �Ǹ� enemy1�� notClear ���� ������.
            if (clearEnemy == 0)
            {
                enemyManager.SetActive(true);
                enemy1NotClear.SetActive(true);
            }
            // ������ treasure�� clearEnemy1�� 0�� �ƴ϶�� clear���� ������
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
