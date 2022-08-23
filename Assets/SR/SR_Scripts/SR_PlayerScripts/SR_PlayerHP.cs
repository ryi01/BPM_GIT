using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SR_PlayerHP : MonoBehaviour
{
    public int hp = 100;
    public int maxHp = 100;
    // public Slider hpSlider;
    public Text hpText;
    public Text maxHpText;

    public Image[] hpImage;


    void Start()
    {
        hp = maxHp;

    }

    public GameObject bossRoom;
    Boss boss;
    void Update()
    {
        if (bossRoom.activeSelf == true)
        {
            boss = GameObject.Find("_Boss").GetComponent<Boss>();
        }
        for (int i=0;i<4;i++)
        {
            if(hp == 25 * (i+1))
            {
                for(int j=0;j<i+1;j++)
                {
                    hpImage[j].gameObject.SetActive(true);
                }
                for(int j=i+1;j<4;j++)
                {
                    hpImage[j].gameObject.SetActive(false);
                }
            }
            else if(hp == 0)
            {
                for(int j=0;j<4;j++)
                {
                    hpImage[j].gameObject.SetActive(false);
                }
            }
        }

        //hp = PlayerPrefs.GetInt("HP");

        if (hp<=0)
        {
            hp = 0;
            //GameOver
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("GameOver");
            
        }

        hpText.text = hp + " ";
        maxHpText.text = " " + maxHp;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Bullet"))
        {

            if (hp >= 25) hp -= 25;
            else hp = 0;
            Destroy(other.gameObject);
            //PlayerPrefs.SetInt("HP", hp);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.name.Contains("Right") || other.gameObject.name.Contains("Left")))
        {


            if (boss.attack == 1 || boss.attack1 == 1)
            {
                hp -= 25;
            }

        }
    }

    public void AddHP()
    {
        hp += 25;
        if (hp > 100) hp = 100;
        //PlayerPrefs.SetInt("HP", hp);

    }
    public void AddBigHP()
    {
        hp += 50;
        if (hp > 100) hp = 100;
        //PlayerPrefs.SetInt("HP", hp);

    }
}
