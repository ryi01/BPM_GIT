using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_PlayerHP : MonoBehaviour
{
    public int hp;
    int maxHp = 100;
    public Slider hpSlider;
    public Text hpText;
    public Text maxHpText;

    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = (float)hp / (float)maxHp;
        hpText.text = hp + " ";
        maxHpText.text = "/ " + maxHp;
    }

    public void AddDamage()
    {
        if (hp > 0)
        {
            hp -= 25;
        }
        if (hp <= 0)
        {
            //Game Over
        }
    }

    public void AddHP()
    {
        hp += 25;
        if (hp > 100) hp = 100;
    }
}
