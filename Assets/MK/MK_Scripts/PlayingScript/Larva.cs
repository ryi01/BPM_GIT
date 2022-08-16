using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애벌레 : 느리게 계속 점프하면서 플레이어를 따라다님
public class Larva : MonoBehaviour
{
    // 애벌레 속도
    public float speed = 3.0f;
    // 점프 힘
    public float jumpPow = 10;
    // 점프 하는 시간
    public float jumpTime = 1;
    // 플레이어와의 거리
    public float moveDis = 2;
    // 넉백 힘
    public float backPow = 3;
    // 공격 시간
    public float attackTime = 2;

    // FSM
    enum LarvaState
    {
        Move,
        Attack
    }
    LarvaState state;
    // 플레이어 위치
    Transform player;
    // 리지드바디
    Rigidbody rigid;
    // 방향
    Vector3 dir;
    // 시간
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Player").transform;
        // 리지드바디 가져오기
        rigid = GetComponent<Rigidbody>();
        // 체력
        // 적 체력 세팅
        LarvaHP hp = GetComponent<LarvaHP>();
        hp.ENEMYHP = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == LarvaState.Move)
        {
            LarvaMove();
        }
        else if(state == LarvaState.Attack)
        {
            LarvaAttack();
        }

    }
    // 플레이어를 향해 움직임
    void LarvaMove()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // 방향 설정
        dir = player.position - transform.position;
        dir.Normalize();

        // 플레이어 향하기
        transform.position += dir * speed * Time.deltaTime;
        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis < 2)
        {
            state = LarvaState.Attack;
        }
    }
    // 플레이어 공격
    void LarvaAttack()
    {
        currentTime += Time.deltaTime;
        if(currentTime > attackTime)
        {
            player.GetComponent<SR_PlayerHP>().hp -= 25;
            state = LarvaState.Move;
            currentTime = 0;
        }
    }

    // 넉백용 함수
    public void NockBack()
    {
        rigid.AddForce(-dir * backPow, ForceMode.Impulse);
    }

    // 땅에 있을때 점프
    // 플레이어와 가까우면 공격
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Plane"))
        {
            rigid.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        }
    }
}
