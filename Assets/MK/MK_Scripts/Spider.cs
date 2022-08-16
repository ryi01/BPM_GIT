using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    // 점프 힘
    public float jumpPow = 2;
    // 필요속성 : 현재 시간
    float currentTime = 0;
    // 속도
    public float speed = 4;
    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // 멈추는 시간
    public float stopTime = 2;
    // 달리는 시간
    public float runTime = 4;
    // 플레이어와의 방향
    Vector3 runDir;
    // y속도
    float yVelocity;
    // 리지드바디
    Rigidbody sRigid;
    // 플레이어와의 거리
    float dis;
    // ����
    float rhythmTime;

    enum SpiderState
    {
        Move,
        Run,
        Jump,
        Stop,
        Set,
        Attack
    }
    SpiderState state;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player");
        state = SpiderState.Move;
        sRigid = GetComponent<Rigidbody>();

        // 적 체력 세팅
        SpiderHP spider = GetComponent<SpiderHP>();
        spider.ENEMYHP = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �÷��̾������� ����
        dir = player.transform.position - transform.position;
        dir.y = transform.position.y;
        dir.Normalize();
        // 중력 넣기
        yVelocity += -9.8f * Time.deltaTime;
        // ���� ����
        rhythmTime += Time.deltaTime;

        if (state == SpiderState.Move)
        {
            SpidexrMove();
        }
        else if (state == SpiderState.Stop)
        {
            SpiderStop();
        }
        else if (state == SpiderState.Run)
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
        else if (state == SpiderState.Attack)
        {
            SpiderAttack();
        }

        if (state == SpiderState.Jump)
        {
            yVelocity = jumpPow;
        }

        dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis < 3)
        {
            state = SpiderState.Attack;
        }

    }

    // 기본적인 움직임
    // 플레이어쪽으로 향하다가 랜덤 시간에 Stop으로 바뀐다
    private void SpidexrMove()
    {
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
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
        if (currentTime > stopTime * 0.3375f)
        {
            // �÷��̾� ����
            runDir = player.transform.position - transform.position;
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

        transform.position += runDir * (speed + 5f) * Time.deltaTime;
        if (currentTime >= runTime * 0.3375f)
        {
            currentTime = 0;
            state = SpiderState.Set;
        }
    }
    private void SpiderJump()
    {
        // Ư���������� �����ؾ���
        runDir.y = yVelocity;

        transform.position += runDir * speed * Time.deltaTime;

    }

    // 가만히 서서 플레이어 바라보기
    private void SpiderSet()
    {
        LookPlayer();

        currentTime += Time.deltaTime;
        if (currentTime > 0.3375f * 10)
        {
            state = SpiderState.Move;
            currentTime = 0;
        }
    }
    // 근접 공격하기
    private void SpiderAttack()
    {
        if (rhythmTime > 0.3375f)
        {
            player.GetComponent<SR_PlayerHP>().hp -= 25;
            rhythmTime = 0;
        }

        if(dis >= 3)
        {
            state = SpiderState.Move;
        }

    }
    void LookPlayer()
    {
        // �÷��̾� ����
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
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
        state = SpiderState.Set;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Contains("Plane") && state == SpiderState.Jump)
        {
            // set상태로 변경
            state = SpiderState.Set;
        }
        if (collision.gameObject.name.Contains("Player") && (state == SpiderState.Jump || state == SpiderState.Run))
        {
            player.GetComponent<SR_PlayerHP>().hp -= 25;
            // set���·� ����
            state = SpiderState.Set;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            state = SpiderState.Set;
        }
    }
}
