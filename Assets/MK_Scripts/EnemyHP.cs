using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� HP ���� 
public class EnemyHP : MonoBehaviour
{
    // �̱���
    public static EnemyHP instance;
    private void Awake()
    {
        instance = this;
    }
    // ü��
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
