using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자 따라 다니기
public class FollowBullet : MonoBehaviour
{
    // 속도 
    public float speed = 20;
    // 방향
    Vector3 dir;
    // 플레이어
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 방향
        dir = player.transform.position - transform.position;
        dir.Normalize();

        // 움직이기
        transform.position += speed * dir * Time.deltaTime;
    }
    // 플레이어 충돌시 죽기
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            Destroy(gameObject);
        }
    }
}
