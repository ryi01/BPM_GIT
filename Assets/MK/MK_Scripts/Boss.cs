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
    #region ����
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
    #endregion

    #region public ����
    // �����̴� �ӵ�
    public float bossSpeed = 5;
    // �����̴� �ð�
    public float moveTime = 4;
    // �Ѿ˰���
    public GameObject bulletFact;
    public GameObject fastBulletFact;
    public GameObject followBulletFact;
    public GameObject followBulletFact1;
    // Attack1�� 
    public GameObject right;
    public GameObject left;
    #endregion
    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // �ð�
    float currentTime;
    // ����
    float rhythmTime;
    Vector3 pos;
    float x;
    float y;
    float z;
    // �Ѿ� ����
    int count;
    Color r_mat;
    Color l_mat;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player");
        // ���¼���
        state = BossState.Move;
        // ü�� ����
        BossHP boss = GetComponent<BossHP> ();
        boss.ENEMYHP = 20;
        right.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        // �޽� ������ ã��
        r_mat = right.GetComponent<MeshRenderer>().material.color;
        l_mat = left.GetComponent<MeshRenderer>().material.color;
    }
    private void FixedUpdate()
    {
        rhythmTime += Time.deltaTime;
    }
    // Update is called once per frame
    void LateUpdate()
    {

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
        // y�� ����
        float y = UnityEngine.Random.Range(2, 6);
        // �÷��̾���� ����
        Vector3 pPos = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z);
        dir = pPos - transform.position;
        dir.Normalize();

        transform.position += dir * bossSpeed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis < 8f)
        {
            state = BossState.Rand;
            currentTime = 0;
        }
        
    }
    // �÷��̾ ���ϴٰ� �������� �����̱�
    private void BossRand()
    {
        // ������ ���� ã��
        currentTime += Time.deltaTime;
        if (currentTime < 0.3375f * 0.2f)
        {
            x = UnityEngine.Random.Range(-20, 20);
            y = UnityEngine.Random.Range(8, 13);
            z = UnityEngine.Random.Range(-20, 20);
        }
        // ������ ���� ��ġ ���ϱ�
        pos = player.transform.position + new Vector3(-x, y, -z);
        // ���������� �� ������ ���� ã��
        Vector3 rndDir = pos - transform.position;
        rndDir.Normalize();
        // �����̱�
        transform.position += rndDir * bossSpeed * Time.deltaTime;
        // ������ ������ ���������
        float dis = Vector3.Distance(pos, transform.position);
        if (dis < 1f)
        {
            // State ��ȭ�ֱ�
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

        // ������ �����ϱ�
        int rnd = UnityEngine.Random.Range(0, 5);

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

    }

    // ���� ��, ������ ���߰� �÷��̾� �ٶ󺸱�
    private void BossStop()
    {
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        // ���� ��, ���߱�
        transform.position += dir * 0 * Time.deltaTime;
        // �����ð� �Ŀ� Move�� ����
        currentTime += Time.deltaTime;
        if(currentTime > 0.3375f * 6)
        {
            currentTime = 0;
            state = BossState.Move;
        }
    }
    int countAttack = 0;
    // ���� 1 : �ݹ� ���� => ���̴� ���
    private void BossAttack1()
    {
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        currentTime += Time.deltaTime;
        if(currentTime <= 0.3375f * 10)
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(true);
            LookBoss(mySight);
        }
        else if(currentTime > 0.3375f * 10 && currentTime <= 0.3375f * 20)
        {
            right.gameObject.SetActive(false);
            left.gameObject.SetActive(true);
            LookBoss(mySight);
        }
        else if(currentTime > 0.3375f * 20)
        {
            countAttack++;
            currentTime = 0;
        }

        if(countAttack == 2)
        {
            currentTime = 0;
            left.gameObject.SetActive(false);
            countAttack = 0;
            state = BossState.Move;
        }
    }

    // ���� 2 : ����ź �߻�
    private void BossAttack2()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.3375f * 4)
        {
            MakingBullet(3, 0.3375f * 0.5f, followBulletFact);
            currentTime = 0;
        }

    }

    // ���� 3 : ���� �Ѿ� 4�� �߻�
    private void BossAttack3()
    {
        // �Ѿ� �����
        MakingBullet(4, 0.3375f, bulletFact);
    }

    // ���� 4 : ���� �ӵ��� �Ѿ� 5�� �߻�
    private void BossAttack4()
    {
        int n = UnityEngine.Random.Range(5, 9);
        // �Ѿ� �����
        MakingBullet(n, 0.3375f * 0.5f, fastBulletFact);
        currentTime = 0;
    }
    // ���� 5 : ����ź + ���� �Ѿ� 1�� �߻�
    private void BossAttack5()
    {
        if (rhythmTime > 0.3375f * 10)
        {
            // �Ѿ��� �����
            GameObject bullet = Instantiate(followBulletFact1);
            // �Ѿ��� �� ��ġ�� ������ ����
            bullet.transform.position = transform.position;
            // State ����
            state = BossState.Stop;
            rhythmTime = 0;
        }
    }

    void MakingBullet(int n,float time,GameObject bulletFactory)
    {
        // ����ź 3���� �����
        for (int i = 0; i < n; i++)
        {
            // 0.3�ʿ�
            if (rhythmTime > time)
            {
                // �Ѿ��� 1�� �����
                GameObject bullet = Instantiate(bulletFactory);
                // ��ġ�� �����ϰ�
                bullet.transform.position = transform.position;
                // �Ѿ� ������ ����
                count++;
                // ���� Ÿ���� 0���� �����
                rhythmTime = 0;
            }
        }
        // �Ѿ��� 3�����
        if (count == n)
        {
            count = 0;
            // ������Ʈ ����
            state = BossState.Stop;
        }
    }

    void LookBoss(Vector3 mySight)
    {
        if (currentTime > 0.3375 * 6)
        {
            Vector3 point = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.LookAt(point);
        }
        else
        {
            transform.LookAt(mySight);
        }
    }
}
