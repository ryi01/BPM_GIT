using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 利 HP 包府 
public class BatHP : MonoBehaviour
{
    // 教臂沛
    public static BatHP instance;
    private void Awake()
    {
        instance = this;
    }
    // 眉仿
    int enemyHP;
    public int ENEMYHP
    {
        get { return enemyHP; }
        set
        {
            enemyHP = value;
            if (enemyHP <= 0)
            {
                Rigidbody rigid = GetComponent<Rigidbody>();
                rigid.useGravity = true;
                Destroy(gameObject, 3);
            }
            
        }
    }
    public void AddDamage(int damage)
    {
        ENEMYHP -= damage;
    }
}
