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
    // 内牢
    public GameObject coinFact;
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
                Destroy(gameObject, 1.5f);
            }
            
        }
    }
    private void OnDestroy()
    {
        int rnd = UnityEngine.Random.Range(0, 2);
        if (rnd == 0)
        {
            GameObject coin = Instantiate(coinFact);
            coin.transform.position = transform.position;
        }
    }

    public void AddDamage(int damage)
    {
        ENEMYHP -= damage;
    }
}
