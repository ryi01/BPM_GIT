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
        Die
    }
    BossState state;
    #endregion

    #region public ����

    [SerializeField]
    public Transform[] followPos = new Transform[3];
    public Transform[] attack4Pos = new Transform[2];

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

    public float time = 0.3409f;

    int pattern = 0;

    Rigidbody rigid;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player");
        // ���¼���
        state = BossState.Move;
        // ���� 1
        right.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        rigid = GetComponent<Rigidbody>();
        GameManager.Instance.m_state = GameManager.GameState.Stop;

        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        rhythmTime += Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.m_state != GameManager.GameState.Playing)
        {
            return;
        }
        if (GetComponent<BossHP>().ENEMYHP > 0)
        {

            // FSM
            if (state == BossState.Move)
            {
                BossMove();
            }
            else if (state == BossState.Rand)
            {
                BossRand();
            }
            else if (state == BossState.Set)
            {
                BossSet();
            }
            else if (state == BossState.Stop)
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
    }

    // �÷��̾ ���� ������
    private void BossMove()
    {
        // y�� ����
        float y = UnityEngine.Random.Range(4, 6);
        // �÷��̾���� ����
        Vector3 pPos = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z);
        dir = pPos - transform.position;
        dir.Normalize();

        transform.position += dir * bossSpeed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, pPos);
        if(dis < 10f)
        {
            state = BossState.Rand;
            currentTime = 0;
        }
        anim.Play("Move");
        
    }
    // �÷��̾ ���ϴٰ� �������� �����̱�
    private void BossRand()
    {
        // ������ ���� ã��
        currentTime += Time.deltaTime;
        if (currentTime < time * 0.2f)
        {
            x = UnityEngine.Random.Range(-20, 20);
            y = UnityEngine.Random.Range(6, 12);
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
        if (dis < 0.1f)
        {
            // State ��ȭ�ֱ�
            state = BossState.Set;
            currentTime = 0;
        }
    }

    // ������ ���߰� �÷��̾� �ٶ󺸱�
    private void BossSet()
    {
        
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        // ���߰�
        transform.position += dir * 0 * Time.deltaTime;
        rigid.velocity = new Vector3(0, 0, 0);

        if (pattern == 0)
        {
            state = BossState.Attack1;
            pattern++;
        }
        else if (pattern == 1)
        {
            state = BossState.Attack2;
            pattern++;
        }
        else if (pattern == 2)
        {
            state = BossState.Attack3;
            pattern++;
        }
        else if (pattern == 3)
        {
            state = BossState.Attack4;
            pattern++;
        }
        else if (pattern == 4)
        {
            state = BossState.Attack5;
            pattern++;
        }
        //state = BossState.Attack4;
        else if (pattern >= 5)
        {
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
    }

    // ���� ��, ������ ���߰� �÷��̾� �ٶ󺸱�
    private void BossStop()
    {
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        // ���� ��, ���߱�
        transform.position += dir * 0 * Time.deltaTime;
        rigid.velocity = new Vector3(0, 0, 0);
        // �����ð� �Ŀ� Move�� ����
        currentTime += Time.deltaTime;
        if(currentTime > time * 6)
        {
            currentTime = 0;
            state = BossState.Move;
        }
    }
    int countAttack = 0;
    public int attack = 0;
    public int attack1 = 0;
    // ���� 1 : �ݹ� ���� => ���̴� ���
    private void BossAttack1()
    {
        anim.Play("BoxAttack");
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        currentTime += Time.deltaTime;
        if(currentTime <= time * 10f)
        {
            attack1 = 0;
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(true);
            LookBoss(mySight);
            if (currentTime >= 9.95f * time && currentTime <= time * 10)
            {
                attack = 1;
            }
        }
        else if(currentTime > time * 10 && currentTime < time * 20f)
        {
            attack = 0;
            right.gameObject.SetActive(false);
            left.gameObject.SetActive(true);
            LookBoss(mySight);
            if (currentTime >= 19.95f * time && currentTime <= time * 20)
            {
                attack1 = 1;
            }

        }

        else if(currentTime > time * 20)
        {
            attack = 0;
            attack1 = 0;
            countAttack++;
            currentTime = 0;
        }


        if(countAttack == 2)
        {
            currentTime = 0;
            left.gameObject.SetActive(false);
            countAttack = 0;
            state = BossState.Stop;
        }
    }

    // ���� 2 : ����ź �߻�
    private void BossAttack2()
    {
        anim.Play("Attack");
        currentTime += Time.deltaTime;
        if (currentTime > time * 4)
        {
            // ����ź 3���� �����
            for (int i = 0; i < 3; i++)
            {
                // 0.3�ʿ�
                if (rhythmTime > time)
                {
                    // �Ѿ��� 1�� �����
                    GameObject bullet = Instantiate(followBulletFact);
                    // ��ġ�� �����ϰ�
                    bullet.transform.position = followPos[count].position;
                    // �Ѿ� ������ ����
                    count++;
                    // ���� Ÿ���� 0���� �����
                    rhythmTime = 0;
                }
            }
            // �Ѿ��� 3�����
            if (count == 3)
            {
                count = 0;
                // ������Ʈ ����
                state = BossState.Stop;
            }
            currentTime = 0;
        }

    }

    // ���� 3 : ���� �Ѿ� 4�� �߻�
    private void BossAttack3()
    {
        anim.Play("Attack");
        // �Ѿ� �����
        MakingBullet(4, time, bulletFact);
    }

    // ���� 4 : ���� �ӵ��� �Ѿ� 5�� �߻�
    private void BossAttack4()
    {
        anim.Play("Attack");
        int n = UnityEngine.Random.Range(5, 9);
        // �Ѿ� �����
        MakingBullet(n, time * 0.5f, fastBulletFact);
    }
    // ���� 5 : ����ź + ���� �Ѿ� 1�� �߻�
    private void BossAttack5()
    {
        anim.Play("Attack");
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        if (rhythmTime > time * 10)
        {
            // �Ѿ��� �����
            GameObject bullet = Instantiate(followBulletFact1);
            // �Ѿ��� �� ��ġ�� ������ ����
            bullet.transform.position = followPos[1].transform.position;
            // State ����
            state = BossState.Stop;
            rhythmTime = 0;
        }
    }

    void MakingBullet(int n,float time,GameObject bulletFactory)
    {
        // �÷��̾� �ٶ󺸱�
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        // ����ź 3���� �����
        for (int i = 0; i < n; i++)
        {
            // 0.3�ʿ�
            if (rhythmTime > time)
            {
                // �Ѿ��� 1�� �����
                GameObject bullet = Instantiate(bulletFactory);
                if (state == BossState.Attack4)
                {
                    bullet.transform.position = attack4Pos[count % 2].position;
                }
                else
                {
                    // ��ġ�� �����ϰ�
                    bullet.transform.position = transform.position;
                }
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
        if (currentTime > time * 6)
        {
            Vector3 point = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.LookAt(point);
        }
        else
        {
            transform.LookAt(mySight);
        }
    }
    public void Die()
    {
        right.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
    }
    // ���� �ε����� ���߰� �����ϱ�
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            rigid.velocity = new Vector3(0, 0, 0);
            transform.position += dir * 0 * Time.deltaTime;
            state = BossState.Stop;
        }
    }
}
