using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 HP 관리 
public class BatHP : MonoBehaviour
{
    Treasure tre;
    // 코인
    public GameObject coinFact;
    // 애니메이터
    Animator anim;
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
                Rigidbody rigid = GetComponent<Rigidbody>();
                rigid.useGravity = true;
                rigid.velocity = new Vector3(0, 0, 0);
                anim.SetTrigger("Die");
                Destroy(gameObject, 1.3f);
            }

        }
    }
    private void Start()
    {
        tre = GameObject.Find("Treasure").GetComponent<Treasure>();
        anim = GetComponentInChildren<Animator>();
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
