using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 利 HP 包府 
public class LarvaHP : MonoBehaviour
{
    // 教臂沛
    public static LarvaHP instance;
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
                Destroy(gameObject);
            }
            
        }
    }
}
