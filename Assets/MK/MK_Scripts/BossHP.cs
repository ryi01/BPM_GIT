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
    float enemyHP;
    // 리지드바디
    Rigidbody rigid;

    public float ENEMYHP
    {
        get { return enemyHP; }
        set
        {
            enemyHP = value;
            bossBar.fillAmount = enemyHP / maxHP;

            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
    public void AddDamage(int damage, Vector3 dir)
    {
        ENEMYHP -= damage;
        rigid.AddForce(-dir * 1.5f, ForceMode.Impulse);
    }
    private void Start()
    {
        ENEMYHP = maxHP;
        rigid = GetComponent<Rigidbody>();
    }
}
