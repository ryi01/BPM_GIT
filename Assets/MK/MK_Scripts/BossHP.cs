using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �� HP ���� 
public class BossHP : MonoBehaviour
{
    // ü�¹�
    public Image bossBar;
    // �ִ� ü��
    public float maxHP = 20;

    // ü��
    float enemyHP;
    // ������ٵ�
    Rigidbody rigid;

    public float ENEMYHP
    {
        get { return enemyHP; }
        set
        {
            enemyHP = value;
            bossBar.fillAmount = enemyHP / maxHP;

            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
    public void AddDamage(int damage, Vector3 dir)
    {
        ENEMYHP -= damage;
        rigid.AddForce(-dir * 1.5f, ForceMode.Impulse);
    }
    private void Start()
    {
        ENEMYHP = maxHP;
        rigid = GetComponent<Rigidbody>();
    }
}
