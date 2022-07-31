using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 만들기
// 1. 무작위로 움직임
// 2. 가만히 서서 공격함
// 패턴 1 : 반반 공격
// 패턴 2 : 유도탄 3개 => 총알 파괴될 수 있어야함
// 패턴 3 : 랜덤 속도를 총알 최대 5개 발사

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

    // 움직이는 속도
    public float bossSpeed = 5;
    // 움직이는 시간
    public float moveTime = 4;


    // 플레이어
    GameObject player;
    // 방향
    Vector3 dir;
    // 시간
    float currentTime;
    Vector3 pos;
    float x;
    float y;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Dummy_Player");
        // 상태설정
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

    // 플레이어를 향해 움직임
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


    // 움직임 멈추고 플레이어 바라보기
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

    // 패턴 1 : 반반 공격
    private void BossAttack1()
    {
        Debug.Log("Attak1");
    }

    // 패턴 2 : 유도탄 발사
    private void BossAttack2()
    {
        Debug.Log("Attack2");
    }

    // 패턴 3 : 랜덤한 속도의 총알을 랜덤하게 발사
    private void BossAttack3()
    {
        Debug.Log("Attack3");
    }
}
