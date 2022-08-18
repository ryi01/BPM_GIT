using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SR_toEnding : MonoBehaviour
{
    GameObject boss;
    Image white;
    private bool checkbool = false;

    private void Start()
    {
        white = GetComponent<Image>();
        white.gameObject.SetActive(false);
    }
    private void Update()
    {
        boss = GameObject.Find("_Boss");


        if (boss == null)
        {
            print("111111");
            white.color = new (255,255,255,0);
            white.gameObject.SetActive(true);
            checkbool = true;
            print("222222");
            StartCoroutine("MainSplash");
            print("555555");
        }
        else checkbool = false;
        SceneManager.LoadScene("Ending");
    }

    IEnumerator MainSplash()
    {
        Color color = white.color;
        print("3333333");
        for(int i=0;i<=100;i++)
        {
            print("44444444");
            color.a += Time.deltaTime * 0.01f;
            white.color = color;
            if (white.color.a >= 0) checkbool = true;
        }
        yield return null;
        
    }
}
