using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 利 HP 包府 
public class EnemyHP : MonoBehaviour
{
    // 教臂沛
    public static EnemyHP instance;
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
            
        }
    }
}
