using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 HP 관리 
public class BossHP : MonoBehaviour
{
    // 체력
    int enemyHP;
    public int ENEMYHP
    {
        get { return enemyHP; }
        set
        {
            enemyHP = value;
            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
    public void AddDamage(int damage)
    {
        ENEMYHP -= damage;
    }
}
