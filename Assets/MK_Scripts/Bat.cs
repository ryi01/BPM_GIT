using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 박쥐 : 플레이어와 멀면 다가가기, 일정 거리내에 들어오면 발사, 일정거리 내면 랜덤으로 움직임
public class Bat : MonoBehaviour
{
    // 플레이어와의 거리
    public float pDis = 4;
    // 박쥐 속도
    public float bSpeed = 4;
    // 움직이는 시간
    public float moveTime = 2;
    // 총알 발사시간
    public float shootTime = 3;
    // 총알 공장
    public GameObject bulletFact;

    // 상태 머신
    enum BatState
    {
        Come,
        Move,
    }
    BatState batState;
    // 플레이어
    GameObject player;
    // 방향
    Vector3 dir;
    // 시간
    float currentTime;
    // 총알 발사용 시간
    float currentTime2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Dummy_Player");

        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);

        // 플레이어를 향하는 방향
        dir = player.transform.position - transform.position;
        dir.Normalize();
        // FSM
        if(batState == BatState.Move)
        {
            BatMove();
        }
        else if(batState == BatState.Come)
        {
            BatCome();
        }

        // 총알발사하기
        currentTime2 += Time.deltaTime;
        if(currentTime2 > shootTime)
        {
            GameObject bullet = Instantiate(bulletFact);
            bullet.transform.position = transform.position;
            currentTime2 = 0;
        }
        
    }
    // 플레이어를 향해 특정 부분까지 가까워짐
    private void BatCome()
    {
        // 플레이어와의 간격 계산
        float dis = Vector3.Distance(player.transform.position, transform.position);
        // 플레이어와 멀다면
        if(dis > pDis)
        {
            // 플레이어를 향해 움직이기
            transform.position += dir * bSpeed * Time.deltaTime;
        }
        // 플레이어와 가까우면
        else
        {
            // 스테이트 바꾸기
            batState = BatState.Move;
        }
    }
    // 랜덤 좌표
    float x;
    float y;
    float z;
    // 방향 백터 및 위치
    Vector3 batDir;
    Vector3 pos;

    // 플레이어와 가깝다면 공중에서 랜덤으로 움직임
    private void BatMove()
    {
        currentTime += Time.deltaTime;
        // 현재시간이 4초보다 크고 5초보다 작을 때
        if (currentTime > 4 && currentTime < 5)
        {
            // 랜덤 좌표 구하고
            x = UnityEngine.Random.Range(-4, 4);
            y = UnityEngine.Random.Range(1, 4);
            z = UnityEngine.Random.Range(-4, 4);
            // 플레이어 근처에 배포
            pos = player.transform.position + new Vector3(x, y, z);
            // 랜덤 위치 방향 벡터
            batDir = pos - transform.position;
            currentTime = 0;
        }
        // 만약 랜덤위치와 멀다면
        float dis = Vector3.Distance(transform.position, pos);
        if (dis > 0.1f)
        {
            // 움직이기
            transform.position += batDir.normalized * bSpeed * Time.deltaTime;
        }
        
    }

}
