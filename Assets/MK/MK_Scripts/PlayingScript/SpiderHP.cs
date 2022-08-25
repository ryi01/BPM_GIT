using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� HP ���� 
public class SpiderHP : MonoBehaviour
{
    Treasure tre;
    // ����
    public GameObject coinFact;
    Animator anim;
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
                anim.SetTrigger("Die");
                Destroy(gameObject, 1f);
            }

        }
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        tre = GameObject.Find("Treasure").GetComponent<Treasure>();
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
