using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����
// 1. �������� ������
// 2. ������ ���� ������
// ���� 1 : �ݹ� ����
// ���� 2 : ����ź 3�� => �Ѿ� �ı��� �� �־����
// ���� 3 : ���� �ӵ��� �Ѿ� �ִ� 5�� �߻�

public class Boss : MonoBehaviour
{
    // FSM
    enum BossState
    {
        Move,
        Rand,
        Set,
        Attack1,
        Attack2,
        Attack3,
    }
    BossState state;

    // �����̴� �ӵ�
    public float bossSpeed = 5;
    // �����̴� �ð�
    public float moveTime = 4;


    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // �ð�
    float currentTime;
    Vector3 pos;
    float x;
    float y;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Dummy_Player");
        // ���¼���
        state = BossState.Move;
    }

    // Update is called once per frame
    void Update()
    {
        float y = UnityEngine.Random.Range(2, 4);
        Vector3 pPos = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z);
        dir = pPos - transform.position;
        dir.Normalize();

        if (state == BossState.Move)
        {
            BossMove();
        }
        else if(state == BossState.Rand)
        {
            BossRand();
        }
        else if(state == BossState.Set)
        {
            BossSet();
        }
        else if (state == BossState.Attack1)
        {
            BossAttack1();
        }
        else if (state == BossState.Attack2)
        {
            BossAttack2();
        }
        else if (state == BossState.Attack3)
        {
            BossAttack3();
        }
    }

    // �÷��̾ ���� ������
    private void BossMove()
    {
        transform.position += dir * bossSpeed * 2 * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis < 5f)
        {
            state = BossState.Rand;
        }
    }
    private void BossRand()
    {
        currentTime += Time.deltaTime;
        if (currentTime < 0.01f)
        {
            x = UnityEngine.Random.Range(-10, 10);
            y = UnityEngine.Random.Range(6, 8);
            z = UnityEngine.Random.Range(-10, 10);
        }
        pos = player.transform.position + new Vector3(x, y, z);
        Vector3 rndDir = pos - transform.position;
        rndDir.Normalize();
        transform.position += rndDir * bossSpeed * Time.deltaTime;
        float dis = Vector3.Distance(pos, transform.position);
        if(dis < 1f)
        {
            state = BossState.Set;
            currentTime = 0;
        }
    }


    // ������ ���߰� �÷��̾� �ٶ󺸱�
    private void BossSet()
    {
        transform.position += dir * 0 * Time.deltaTime;
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);

        int rnd = UnityEngine.Random.Range(0, 3);
        if(rnd == 0)
        {
            state = BossState.Attack1;
        }
        else if(rnd == 1)
        {
            state = BossState.Attack2;
        }
        else if (rnd == 2)
        {
            state = BossState.Attack3;
        }
    }

    // ���� 1 : �ݹ� ����
    private void BossAttack1()
    {
        Debug.Log("Attak1");
    }

    // ���� 2 : ����ź �߻�
    private void BossAttack2()
    {
        Debug.Log("Attack2");
    }

    // ���� 3 : ������ �ӵ��� �Ѿ��� �����ϰ� �߻�
    private void BossAttack3()
    {
        Debug.Log("Attack3");
    }
}
