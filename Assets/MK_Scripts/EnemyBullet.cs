using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어를 향해 돌격!
public class EnemyBullet : MonoBehaviour
{
    // 속도
    public float bulletSpeed = 4;
    // 방향
    Vector3 dir;
    // 플레이어
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.Find("Player");
        // 방향
        dir = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어를 보기
        transform.LookAt(player.transform.position);
        // 움직이기
        transform.position += dir * bulletSpeed * Time.deltaTime;
    }
}
