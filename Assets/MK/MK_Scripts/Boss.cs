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

    // 움직이는 속도
    public float bossSpeed = 5;
    // 움직이는 시간
    public float moveTime = 4;
    // 총알공장
    public GameObject bulletFact;
    public GameObject fastBulletFact;
    public GameObject followBulletFact;
    public GameObject followBulletFact1;

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

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Player");
        // 상태설정
        state = BossState.Move;
        // 체력 설정
        BossHP boss = GetComponent<BossHP> ();
        boss.ENEMYHP = 20;
    }
    private void FixedUpdate()
    {
        rhythmTime += Time.deltaTime;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // y축 변경
        float y = UnityEngine.Random.Range(2, 6);
        // 플레이어까지 방향
        Vector3 pPos = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z);
        dir = pPos - transform.position;
        // 플레이어 바라보기
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

    // 플레이어를 향해 움직임
    private void BossMove()
    {
        transform.position += dir * bossSpeed  * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis < 8f)
        {
            state = BossState.Rand;
        }
    }
    // 플레이어를 향하다가 랜덤으로 움직이기
    private void BossRand()
    {
        // 랜덤한 지점 찾기
        currentTime += Time.deltaTime;
        if (currentTime < 0.3375f * 0.2f)
        {
            x = UnityEngine.Random.Range(-20, 20);
            y = UnityEngine.Random.Range(8, 13);
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
        if (dis < 1f)
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

    }

    // 공격 후, 움직임 멈추고 플레이어 바라보기
    private void BossStop()
    {
        // 공격 후, 멈추기
        transform.position += dir * 0 * Time.deltaTime;
        // 일정시간 후에 Move로 변경
        currentTime += Time.deltaTime;
        if(currentTime > 0.3375f * 6)
        {
            currentTime = 0;
            state = BossState.Move;
        }
    }

    // 패턴 1 : 반반 공격 => 쉐이더 사용
    private void BossAttack1()
    {
        Debug.Log("Attak1");
    }

    // 패턴 2 : 유도탄 발사
    private void BossAttack2()
    {
        // 시간이 흐름
        currentTime += Time.deltaTime;
        // 3초 이후일 때,
        if (currentTime > 0.3375f * 8)
        {
            // 총알 만들기
            MakingBullet(3, 0.3375f * 2, bulletFact);
            currentTime = 0;
        }
    }

    // 패턴 3 : 느린 총알 4개 발사
    private void BossAttack3()
    {
        // 총알 만들기
        MakingBullet(4, 0.3375f, bulletFact);
    }

    // 패턴 4 : 빠른 속도를 총알 5개 발사
    private void BossAttack4()
    {
        // 총알 만들기
        MakingBullet(5, 0.3375f * 0.5f,fastBulletFact);
    }
    // 패턴 5 : 유도탄 + 빠른 총알 1개 발사
    private void BossAttack5()
    {
        // 총알을 만들고
        GameObject bullet = Instantiate(followBulletFact);
        // 총알을 내 위치에 가져다 놓음
        bullet.transform.position = transform.position;
        // State 변경
        state = BossState.Stop;
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
                // 위치를 조정하고
                bullet.transform.position = transform.position;
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
}
