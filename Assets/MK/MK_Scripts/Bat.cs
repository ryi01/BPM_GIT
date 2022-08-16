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
    public float change = 0.2f;

    // 상태 머신
    enum BatState
    {
        Come,
        Move,
        Stop,
        Back
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
    float currentTime3;

    // 랜덤 좌표
    float x;
    float y;
    float z;
    // 방향 백터 및 위치
    Vector3 batDir;
    Vector3 pos;
    BatHP bat;
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        // 총알발사하기
        currentTime2 += Time.deltaTime;

    }
    // Start is called before the first frame update
    void Start()
    {
        // 적 체력 세팅
        bat = GetComponent<BatHP>();
        bat.ENEMYHP = 1;
        float y = UnityEngine.Random.Range(5, 10);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);
        float rnd = UnityEngine.Random.Range(2, 8);
        // 플레이어를 향하는 방향
        dir = player.transform.position - transform.position;
        dir = new Vector3(dir.x, dir.y + rnd, dir.z);
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
        else if(batState == BatState.Stop)
        {
            BatStop();
        }
        else if (batState == BatState.Back)
        {
            BatBack();
        }

        if (bat.ENEMYHP > 0)
        {
            if (currentTime2 > shootTime * 0.3375f)
            {
                GameObject bullet = Instantiate(bulletFact);
                bullet.transform.position = transform.position;
                currentTime2 = 0;
            }
        }
    }
    // 플레이어를 향해 특정 부분까지 가까워짐
    private void BatCome()
    {
        transform.position += dir * bSpeed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis < pDis)
        {
            batState = BatState.Back;
        }

    }
    int time;
    // 플레이어와 가깝다면 공중에서 랜덤으로 움직임
    private void BatMove()
    {
        time++;
        if(time < 1)
        {
            x = UnityEngine.Random.Range(-10, 10);
            y = UnityEngine.Random.Range(3, 6);
            z = UnityEngine.Random.Range(-10, 10);
        }
        pos = new Vector3(x, y + 2, z);
        batDir = pos - transform.position;
        batDir.Normalize();
        transform.position += batDir * bSpeed * Time.deltaTime;

        float dis = Vector3.Distance(transform.position, pos);
        if(dis < 0.1f)
        {
            time = 0;
            batState = BatState.Come;
        }
    }

    void BatStop()
    {
        transform.position += batDir * 0 * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis > pDis )
        {
            batState = BatState.Move;
        }
    }
    // 가까우면 뒤로 가기
    void BatBack()
    {
        currentTime3 += Time.deltaTime;
        transform.position += -dir * bSpeed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis > pDis && currentTime3 > 0.3375f * 6)
        {
            currentTime = 0;
            batState = BatState.Stop;
        }
    }

    // 벽에 부딪혔을 경우
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            batState = BatState.Stop;
        }
    }
}
