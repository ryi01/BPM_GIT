using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� HP ���� 
public class SpiderHP : MonoBehaviour
{
    // �̱���
    public static SpiderHP instance;
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
            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
