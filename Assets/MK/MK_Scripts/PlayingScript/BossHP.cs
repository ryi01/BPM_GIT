using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적 HP 관리 
public class BossHP : MonoBehaviour
{
    // 체력바
    public Image bossBar;
    // 최대 체력
    public float maxHP = 20;

    // 체력
    public float enemyHP;
    // 리지드바디
    Rigidbody rigid;

    
    public float ENEMYHP
    {
        get { return enemyHP; }
        set
        {
            enemyHP = value;
            bossBar.fillAmount = enemyHP / maxHP;

            if(enemyHP <= 0 && enemyHP > -5)
            {
                bossBar.gameObject.SetActive(false);
                GetComponent<Boss>().Die();
                rigid.velocity = new Vector3(0, 0, 0);
            }

            
            if (enemyHP <= -5)
            {
                Destroy(gameObject,5.0f);
            }
            
        }
    }
    public void AddDamage(int damage, Vector3 dir)
    {
        ENEMYHP -= damage;
        rigid.AddForce(-dir * 0.1f, ForceMode.Impulse);
    }
    private void Start()
    {
        ENEMYHP = maxHP;
        rigid = GetComponent<Rigidbody>();
    }
}
