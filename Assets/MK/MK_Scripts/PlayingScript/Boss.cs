using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 만들기
// 1. 무작위로 움직임
// 2. 가만히 서서 공격함
// 패턴 1 : 반반 공격
// 패턴 2 : 유도탄 3개 => 총알 파괴될 수 있어야함
// 패턴 3 : 느린 속도를 총알 4개 발사
// 패턴 4 : 빠른 속도를 총알 5개 발사
// 패턴 5 : 유도탄 + 빠른 총알 1개 발사

public class Boss : MonoBehaviour
{
    #region 상태
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

    #region public 변수

    [SerializeField]
    public Transform[] followPos = new Transform[3];
    public Transform[] attack4Pos = new Transform[2];

    // 움직이는 속도
    public float bossSpeed = 5;
    // 움직이는 시간
    public float moveTime = 4;
    // 총알공장
    public GameObject bulletFact;
    public GameObject fastBulletFact;
    public GameObject followBulletFact;
    public GameObject followBulletFact1;
    // Attack1번 
    public GameObject right;
    public GameObject left;
    #endregion
    // 플레이어
    GameObject player;
    // 방향
    Vector3 dir;
    // 시간
    float currentTime;
    // 리듬
    float rhythmTime;
    Vector3 pos;
    float x;
    float y;
    float z;
    // 총알 개수
    int count;

    public float time = 0.3409f;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Player");
        // 상태설정
        state = BossState.Move;
        // 공격 1
        right.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        rhythmTime += Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
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

    // 플레이어를 향해 움직임
    private void BossMove()
    {
        // y축 변경
        float y = UnityEngine.Random.Range(4, 6);
        // 플레이어까지 방향
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
        
    }
    // 플레이어를 향하다가 랜덤으로 움직이기
    private void BossRand()
    {
        // 랜덤한 지점 찾기
        currentTime += Time.deltaTime;
        if (currentTime < time * 0.2f)
        {
            x = UnityEngine.Random.Range(-20, 20);
            y = UnityEngine.Random.Range(6, 12);
            z = UnityEngine.Random.Range(-20, 20);
        }
        // 랜덤한 지점 위치 정하기
        pos = player.transform.position + new Vector3(-x, y, -z);
        // 랜덤지점과 내 지점의 방향 찾기
        Vector3 rndDir = pos - transform.position;
        rndDir.Normalize();
        // 움직이기
        transform.position += rndDir * bossSpeed * Time.deltaTime;
        // 랜덤한 지점에 가까워지면
        float dis = Vector3.Distance(pos, transform.position);
        if (dis < 0.1f)
        {
            // State 변화주기
            state = BossState.Set;
            currentTime = 0;
        }
    }

    // 움직임 멈추고 플레이어 바라보기
    private void BossSet()
    {
        // 멈추고
        transform.position += dir * 0 * Time.deltaTime;
        // 플레이어 바라보기
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);

        // 랜덤한 공격하기
        int rnd = UnityEngine.Random.Range(0, 5);
        //state = BossState.Attack4;

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

    // 공격 후, 움직임 멈추고 플레이어 바라보기
    private void BossStop()
    {
        // 플레이어 바라보기
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        // 공격 후, 멈추기
        transform.position += dir * 0 * Time.deltaTime;
        // 일정시간 후에 Move로 변경
        currentTime += Time.deltaTime;
        if(currentTime > time * 6)
        {
            currentTime = 0;
            state = BossState.Move;
        }
    }
    int countAttack = 0;
    // 패턴 1 : 반반 공격 => 쉐이더 사용
    private void BossAttack1()
    {
        // 플레이어 바라보기
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        currentTime += Time.deltaTime;
        if(currentTime <= time * 10)
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(true);
            LookBoss(mySight);
        }
        else if(currentTime > time * 10 && currentTime <= time * 20)
        {
            right.gameObject.SetActive(false);
            left.gameObject.SetActive(true);
            LookBoss(mySight);
        }
        else if(currentTime > time * 20)
        {
            countAttack++;
        }

        if(countAttack == 2)
        {
            currentTime = 0;
            left.gameObject.SetActive(false);
            countAttack = 0;
            state = BossState.Move;
        }
    }

    // 패턴 2 : 유도탄 발사
    private void BossAttack2()
    {
        currentTime += Time.deltaTime;
        if (currentTime > time * 4)
        {
            // 유도탄 3개를 만들기
            for (int i = 0; i < 3; i++)
            {
                // 0.3초에
                if (rhythmTime > time)
                {
                    // 총알을 1개 만들고
                    GameObject bullet = Instantiate(followBulletFact);
                    // 위치를 조정하고
                    bullet.transform.position = followPos[count].position;
                    // 총알 개수를 세고
                    count++;
                    // 리듬 타임을 0으로 만든다
                    rhythmTime = 0;
                }
            }
            // 총알이 3개라면
            if (count == 3)
            {
                count = 0;
                // 스테이트 변경
                state = BossState.Stop;
            }
            currentTime = 0;
        }

    }

    // 패턴 3 : 느린 총알 4개 발사
    private void BossAttack3()
    {
        // 총알 만들기
        MakingBullet(4, time, bulletFact);
    }

    // 패턴 4 : 빠른 속도를 총알 5개 발사
    private void BossAttack4()
    {
        int n = UnityEngine.Random.Range(5, 9);
        // 총알 만들기
        MakingBullet(n, time * 0.5f, fastBulletFact);
    }
    // 패턴 5 : 유도탄 + 빠른 총알 1개 발사
    private void BossAttack5()
    {
        if (rhythmTime > time * 10)
        {
            // 총알을 만들고
            GameObject bullet = Instantiate(followBulletFact1);
            // 총알을 내 위치에 가져다 놓음
            bullet.transform.position = transform.position + new Vector3(0, 5, 0);
            // State 변경
            state = BossState.Stop;
            rhythmTime = 0;
        }
    }

    void MakingBullet(int n,float time,GameObject bulletFactory)
    {
        // 유도탄 3개를 만들기
        for (int i = 0; i < n; i++)
        {
            // 0.3초에
            if (rhythmTime > time)
            {
                // 총알을 1개 만들고
                GameObject bullet = Instantiate(bulletFactory);
                if (state == BossState.Attack4)
                {
                    bullet.transform.position = attack4Pos[count % 2].position;
                }
                else
                {
                    // 위치를 조정하고
                    bullet.transform.position = transform.position;
                }
                // 총알 개수를 세고
                count++;
                // 리듬 타임을 0으로 만든다
                rhythmTime = 0;
            }
        }
        // 총알이 3개라면
        if (count == n)
        {
            count = 0;
            // 스테이트 변경
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
    // 벽에 부딪히면 멈추고 공격하기
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            transform.position += dir * 0 * Time.deltaTime;
            // 랜덤한 공격하기
            int rnd = UnityEngine.Random.Range(0, 5);
            //state = BossState.Attack4;

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
}
