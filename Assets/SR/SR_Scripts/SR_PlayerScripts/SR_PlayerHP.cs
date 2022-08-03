using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_PlayerHP : MonoBehaviour
{
    public int hp=100;
    int maxHp = 100;
    public Slider hpSlider;
    public Text hpText;
    public Text maxHpText;


    void Start()
    {
        hp = PlayerPrefs.GetInt("HP");
    }

    void Update()
    {
        
        hpSlider.value = (float)hp / (float)maxHp;
        hpText.text = hp + " ";
        maxHpText.text = "/ " + maxHp;

        if(hp<=0)
        {
            hp = 0;
            //GameOver
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Bullet"))
        {
            hp -= 25;
            PlayerPrefs.SetInt("HP", hp);

        }
    }

    public void AddHP()
    {
        hp += 25;
        if (hp > 100) hp = 100;
        PlayerPrefs.SetInt("HP", hp);

    }
    public void AddBigHP()
    {
        hp += 50;
        if (hp > 100) hp = 100;
        PlayerPrefs.SetInt("HP", hp);

    }
}
