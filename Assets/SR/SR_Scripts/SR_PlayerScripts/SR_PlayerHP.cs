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

    void Update()
    {
        hpSlider.value = (float)hp / (float)maxHp;
        hpText.text = hp + " ";
        maxHpText.text = "/ " + maxHp;

        if(hp<=0)
        {
            hp = 0;
        }
    }

    
    // bullet�� �������� enemy�̸� �κ��� �̸��� �ƴ϶� layer�� ���ؼ� �ؾ��� ��
    // bullet -> Bullet���� �����߾�
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Bullet"))
        {
            hp -= 25;
        }
        
    }

    public void AddHP()
    {
        hp += 25;
        if (hp > 100) hp = 100;
    }
}
