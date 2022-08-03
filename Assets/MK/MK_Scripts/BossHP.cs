using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� HP ���� 
public class BossHP : MonoBehaviour
{
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
    private void OnDestroy()
    {
        Treasure tre = GameObject.Find("Treasure").GetComponent<Treasure>();
        tre.count++;
    }
    public void AddDamage(int damage)
    {
        ENEMYHP -= damage;
    }
}
