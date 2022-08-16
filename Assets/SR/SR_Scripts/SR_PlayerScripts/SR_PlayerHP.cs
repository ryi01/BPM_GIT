using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_PlayerHP : MonoBehaviour
{
    public int hp=100;
    public int maxHp = 100;
    // public Slider hpSlider;
    public Text hpText;
    public Text maxHpText;


    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        hp = PlayerPrefs.GetInt("HP");
        
        // hpSlider.value = (float)hp / (float)maxHp;
        // hpText.text = hp + " ";
        // maxHpText.text = "/ " + maxHp;

        if(hp<=0)
        {
            hp = 0;
            //GameOver
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("HP", maxHp);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Bullet"))
        {   
            //ġƮ
            //hp -= 25;
            PlayerPrefs.SetInt("HP", hp);

        }
        if(other.gameObject.name.Contains("Right") || other.gameObject.name.Contains("Left"))
        {
            float currentTime = 0;
            currentTime += Time.deltaTime;
            //if (currentTime > 0.3375f) hp -= 25;
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
