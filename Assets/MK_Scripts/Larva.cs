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

    // 플레이어 위치
    Transform player;
    // 리지드바디
    Rigidbody rigid;
    // 방향
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Pos").transform;
        // 리지드바디 가져오기
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // 방향 설정
        dir = player.position - transform.position;
        dir.Normalize();

        // 플레이어 향하기
        transform.position += dir * speed * Time.deltaTime;
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
        if (collision.gameObject.name.Contains("Floor"))
        {
            rigid.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        }
        if (collision.gameObject.name.Contains("Player"))
        {
            NockBack();
        }
    }
}
