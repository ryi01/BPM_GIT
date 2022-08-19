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
    int count=0;
    public GameObject quad;

    private void Start()
    {
        white = GetComponent<Image>();
        white.color = new(255, 255, 255, 0);

    }
    private void Update()
    {
        count = quad.GetComponent<SR_Enemy2ToBoss>().count;


        if(count==1)
        {
            boss = GameObject.Find("_Boss");

            if (boss == null)
            {
                checkbool = true;
                print("11111111");
                StopAllCoroutines();
                StartCoroutine("MainSplash");
                
            }

            else checkbool = false;
            

        }
        
    }

    IEnumerator MainSplash()
    {
        Color color = white.color;

        //yield return new WaitForSeconds(1.0f);

        while (color.a <=1) 
        {
            color.a += 0.01f;
            white.color = color;
            if (white.color.a >= 0) checkbool = true;
            yield return null;
        }
        
        SceneManager.LoadScene("Ending");

    }
}
