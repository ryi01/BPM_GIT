using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 거미 : 랜덤 시간마다 플레이어의 위치로 점프하거나 돌진함
// FSM 사용해서 Move, Jump, Run으로 바꿔야할듯
// 공격부 추가하기 07.29 아직 안함 => 하면 지워라
public class Spider : MonoBehaviour
{
    // 점프 힘
    public float jumpPow = 2;
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
    // 플레이어와의 방향
    Vector3 runDir;
    // y속도
    float yVelocity;
    // 리지드바디
    Rigidbody sRigid;
    // 플레이어와의 거리
    float dis;

    enum SpiderState
    {
        Move,
        Run,
        Jump,
        Stop,
        Set,
        Back,
        Attack
    }
    SpiderState state;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Pos").GetComponent<Transform>();
        state = SpiderState.Move;
        sRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 플레이어까지의 방향
        dir = player.position - transform.position;
        dir.Normalize();
        // 중력 넣기
        yVelocity += -9.8f * Time.deltaTime;

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
        else if (state == SpiderState.Set)
        {
            SpiderSet();
        }
        else if (state == SpiderState.Back)
        {
            SpiderBack();
        }
        else if(state == SpiderState.Attack)
        {
            SpiderAttack();
        }

        if (state == SpiderState.Jump)
        {
            yVelocity = jumpPow;
        }

    }

    private void Update()
    {
        dis = Vector3.Distance(player.position, transform.position);
        if (dis < 1)
        {
            Debug.Log("Attack");
            state = SpiderState.Attack;
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
        int rndTime = Random.Range(2, 8);
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
        LookPlayer();
        // 일정시간이 지나면
        currentTime += Time.deltaTime;
        if(currentTime > stopTime)
        {
            // 플레이어 방향
            runDir = player.position - transform.position;
            runDir.Normalize();

            int rndAttack = Random.Range(0, 2);
            if (rndAttack == 0)
            {
                state = SpiderState.Run;
            }
            else
            {
                state = SpiderState.Jump;
            }
        }
        
    }
    private void SpiderRun()
    {
        // 일정시간동안 계속 달림 => 플레이어의 위치는 뛰는 순간 고정
        currentTime += Time.deltaTime;

        transform.position += runDir * (speed + 3f) * Time.deltaTime;
        if (currentTime >= runTime)
        {
            currentTime = 0;
            state = SpiderState.Set;
        }
    }
    private void SpiderJump()
    {

        // 특정지점까지 점프해야함
        runDir.y = yVelocity;

        transform.position += runDir * speed * Time.deltaTime;

    }

    // 가만히 서서 플레이어 바라보기
    private void SpiderSet()
    {
        LookPlayer();

        currentTime += Time.deltaTime;
        if(currentTime > 3)
        {
            state = SpiderState.Move;
            currentTime = 0;
        }
    }
    // 근접 공격하기
    private void SpiderAttack()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 1)
        {
            Debug.Log("Attack");
            currentTime = 0;
        }

        if(dis >= 1)
        {
            state = SpiderState.Move;
        }
        
    }
    // 넉백
    void SpiderBack()
    {
        NockBack();

        state = SpiderState.Set;
    }
    void LookPlayer()
    {
        // 플레이어 보기
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // 그 자리에 멈추지만, 플레이어는 바라봐야함.
        transform.position += dir * 0 * Time.deltaTime;
    }


    // 부딪혔을 경우,
    // 넉백 힘
    public float backPow = 3;
    // 넉백용 함수
    public void NockBack()
    {
        sRigid.AddForce(-dir * backPow, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            // 플레이어랑 부딪히면 넉백 => 플레이어와 부딪히면 넉백이 아니라 공격
            state = SpiderState.Back;

        }
        if (collision.gameObject.name.Contains("Floor") && state == SpiderState.Jump)
        {
            // set상태로 변경
            state = SpiderState.Set;
        }
    }

}
