using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����
// 1. �������� ������
// 2. ������ ���� ������
// ���� 1 : �ݹ� ����
// ���� 2 : ����ź 3�� => �Ѿ� �ı��� �� �־����
// ���� 3 : ���� �ӵ��� �Ѿ� 4�� �߻�
// ���� 4 : ���� �ӵ��� �Ѿ� 5�� �߻�
// ���� 5 : ����ź + ���� �Ѿ� 1�� �߻�

public class Boss : MonoBehaviour
{
    // FSM
    enum BossState
    {
        Move,
        Rand,
        Set,
        Stop,
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5,
    }
    BossState state;

    // �����̴� �ӵ�
    public float bossSpeed = 5;
    // �����̴� �ð�
    public float moveTime = 4;
    // �Ѿ˰���
    public GameObject bulletFact;
    public GameObject fastBulletFact;
    public GameObject followBulletFact;
    public GameObject followBulletFact1;

    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // �ð�
    float currentTime;
    float currentTime2;
    Vector3 pos;
    float x;
    float y;
    float z;
    // �Ѿ� ����
    int count;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Dummy_Player");
        // ���¼���
        state = BossState.Move;
        // ü�� ����
        BossHP.instance.ENEMYHP = 20;
    }

    // Update is called once per frame
    void Update()
    {
        // y�� ����
        float y = UnityEngine.Random.Range(2, 4);
        // �÷��̾���� ����
        Vector3 pPos = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z);
        dir = pPos - transform.position;
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        dir.Normalize();
        // FSM
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
        else if(state == BossState.Stop)
        {
            BossStop();
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
        else if (state == BossState.Attack4)
        {
            BossAttack4();
        }
        else if (state == BossState.Attack5)
        {
            BossAttack5();
        }
    }

    // �÷��̾ ���� ������
    private void BossMove()
    {
        transform.position += dir * bossSpeed  * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis < 5f)
        {
            state = BossState.Rand;
        }
    }
    // �÷��̾ ���ϴٰ� �������� �����̱�
    private void BossRand()
    {
        currentTime += Time.deltaTime;
        if (currentTime < 0.01f)
        {
            x = UnityEngine.Random.Range(-20, 20);
            y = UnityEngine.Random.Range(6, 8);
            z = UnityEngine.Random.Range(-20, 20);
        }
        pos = player.transform.position + new Vector3(x, y, z);
        Vector3 rndDir = pos - transform.position;
        rndDir.Normalize();
        transform.position += rndDir * bossSpeed * Time.deltaTime;
        float dis = Vector3.Distance(pos, transform.position);
        if (dis < 1f)
        {
            state = BossState.Set;
            currentTime = 0;
        }
    }

    // ������ ���߰� �÷��̾� �ٶ󺸱�
    private void BossSet()
    {
        // ���߰�
        transform.position += dir * 0 * Time.deltaTime;
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        int rnd = UnityEngine.Random.Range(1, 5);
        if (rnd == 0)
        {
            state = BossState.Attack1;
        }
        if (rnd == 1)
        {
            state = BossState.Attack2;
        }
        else if (rnd == 2)
        {
            state = BossState.Attack3;
        }
        else if (rnd == 3)
        {
            state = BossState.Attack4;
        }
        else if (rnd == 4)
        {
            state = BossState.Attack5;
        }
        print(rnd);
    }

    // ���� ��, ������ ���߰� �÷��̾� �ٶ󺸱�
    private void BossStop()
    {
        // ���� ��, ���߱�
        transform.position += dir * 0 * Time.deltaTime;
        currentTime += Time.deltaTime;
        if(currentTime > 2)
        {
            currentTime = 0;
            state = BossState.Move;
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
        // �ð��� �帧
        currentTime2 += Time.deltaTime;
        // 3�� ������ ��,
        if (currentTime2 > 1)
        {
            MakingBullet(3, 1f,followBulletFact1);
        }
    }

    // ���� 3 : ���� �Ѿ� 4�� �߻�
    private void BossAttack3()
    {
        // �Ѿ� �����
        MakingBullet(4, 0.8f, bulletFact);
    }

    // ���� 4 : ���� �ӵ��� �Ѿ� 5�� �߻�
    private void BossAttack4()
    {
        // �Ѿ� �����
        MakingBullet(5, 0.1f, fastBulletFact);
    }
    // ���� 5 : ����ź + ���� �Ѿ� 1�� �߻�
    private void BossAttack5()
    {
        GameObject bullet = Instantiate(followBulletFact);
        bullet.transform.position = transform.position;
        state = BossState.Stop;
    }

    void MakingBullet(int n, float time,GameObject bulletFactory)
    {
        // �ð��� �帧
        currentTime += Time.deltaTime;
        // ����ź 3���� �����
        for (int i = 0; i < n; i++)
        {
            // 0.3�ʿ� �Ѱ��� �����
            if (currentTime > time)
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = transform.position;
                currentTime = 0;
                count++;
            }
        }
        // �Ѿ��� 3�����
        if (count == n)
        {
            count = 0;
            // ������Ʈ ����
            state = BossState.Stop;
            currentTime2 = 0f;
        }
    }
}
