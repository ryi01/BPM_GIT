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
    // 구
    public static Vector3 insideUnitSpehre;

    // 상태 머신
    enum BatState
    {
        Come,
        Move,
        Stop
    }
    BatState batState;
    // 플레이어
    GameObject player;
    // 방향
    Vector3 dir;
    // 시간
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Dummy_Player");
        dir = player.transform.position - transform.position;
        dir.Normalize();

        if(batState == BatState.Move)
        {
            BatMove();
        }
        else if(batState == BatState.Come)
        {
            BatCome();
        }
        else if (batState == BatState.Stop)
        {
            BatStop();
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
            transform.position += dir * bSpeed * Time.deltaTime;
        }
        else
        {
            batState = BatState.Move;
        }
    }
    float x;
    float y;
    float z;
    Vector3 batDir;
    Vector3 pos;

    // 플레이어와 가깝다면 공중에서 랜덤으로 움직임
    private void BatMove()
    {

        currentTime += Time.deltaTime;
        if (currentTime > moveTime)
        {
            x = UnityEngine.Random.Range(-1, 1);
            y = UnityEngine.Random.Range(2, 5);
            z = UnityEngine.Random.Range(-2, 2);
            pos = new Vector3(x, y, z);
            batDir = pos - transform.position;
            currentTime = 0;
        }
        print(pos);

        transform.position += batDir.normalized * bSpeed * Time.deltaTime;
        float dis = Vector3.Distance(pos, transform.position);
        if(dis < 0.5f)
        {
            batState = BatState.Stop;
        }
    }
    void BatStop()
    {
        transform.position += dir * 0 * Time.deltaTime;
    }

}
