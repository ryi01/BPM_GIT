using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� HP ���� 
public class BatHP : MonoBehaviour
{
    // �̱���
    public static BatHP instance;
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
