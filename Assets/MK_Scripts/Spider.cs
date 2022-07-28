using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 거미 : 랜덤 시간마다 플레이어의 위치로 점프하거나 돌진함
// FSM 사용해서 Move, Jump, Run으로 바꿔야할듯
public class Spider : MonoBehaviour
{
    // 필요속성 : 현재 시간
    float currentTime = 0;
    // 속도
    float speed = 4;
    // 플레이어
    Transform player;
    // 방향
    Vector3 dir;
    // 멈추는 시간
    float stopTime = 2;
    // 달리는 시간
    float runTime = 4;
    Vector3 runDir;
    // 리지드바디
    Rigidbody sRigid;

    enum SpiderState
    {
        Move,
        Run,
        Jump,
        Stop
    }
    SpiderState state;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Dummy_Player").transform;
        state = SpiderState.Move;
        sRigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        dir = player.transform.position - transform.position;
        dir.Normalize();

        if (state == SpiderState.Move)
        {
            SpidexrMove();
        }
        else if (state == SpiderState.Stop)
        {
            SpiderStop();
        }
        else if(state == SpiderState.Run)
        {
            SpiderRun();
        }
        else if (state == SpiderState.Jump)
        {
            SpiderJump();
        }

    }

    // 기본적인 움직임
    // 플레이어쪽으로 향하다가 랜덤 시간에 Stop으로 바뀐다
    private void SpidexrMove()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);
        // 움직임
        transform.position += dir * speed * Time.deltaTime;

        // 랜덤 시간이 되면
        currentTime += Time.deltaTime;
        int rndTime = Random.Range(2, 5);
        if (currentTime > rndTime)
        {
            // stop으로 바뀌기
            state = SpiderState.Stop;
            currentTime = 0;
        }

    }

    // 그 자리에 서서 멈추고 플레이어만 바라보다가 일정 시간이 지난 후, 런이나 점프 모드로 바뀜
    private void SpiderStop()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // 그 자리에 멈추지만, 플레이어는 바라봐야함.
        transform.position += dir * 0 * Time.deltaTime;
        // 일정시간이 지나면
        currentTime += Time.deltaTime;
        if(currentTime > stopTime)
        {
            runDir = player.transform.position - transform.position;
            runDir.Normalize();
            state = SpiderState.Jump;
            /*int rndAttack = Random.Range(0, 1);
            if(rndAttack == 0)
            {
                state = SpiderState.Run;
            }
            else
            {
                state = SpiderState.Jump;
            }*/
        }
        
    }
    private void SpiderRun()
    {

        // 일정시간동안 계속 달림 => 플레이어의 위치는 뛰는 순간 고정
        currentTime += Time.deltaTime;

        transform.position += runDir * (speed + 1f) * Time.deltaTime;
        if (currentTime >= runTime)
        {
            currentTime = 0;
            state = SpiderState.Move;
        }
    }

    private void SpiderJump()
    {
        Debug.Log("Jump");
        sRigid.AddForceAtPosition(Vector3.forward, runDir, ForceMode.Impulse);
    }

 /*   void SpiderAttack(Vector3 dir)
    {        
        // 랜덤으로 할 행동을 고르고
        int rnd = Random.Range(0, 1);
        if (rnd == 0)
        {
            // 랜덤시간마다
            float rndTime = Random.Range(3, 10);
            // 시간이 흐르고
            currentTime += Time.deltaTime;
            if (rndTime < currentTime)
            {
                // 그쪽으로 점프하고 싶다
                transform.position = Vector3.Slerp(transform.position, player.transform.position, 0.05f);
            }
        }
        else
        {
            // 랜덤시간마다
            float rndTime = Random.Range(3, 10);
            // 시간이 흐르고
            currentTime += Time.deltaTime;
            if (rndTime < currentTime)
            {
                // 그쪽으로 점프하고 싶다
                transform.position += dir * speed * 2 * Time.deltaTime;
            }
        }

    }*/
}
