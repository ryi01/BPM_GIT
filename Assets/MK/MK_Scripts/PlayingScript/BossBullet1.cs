using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 느린 속도로 플레이어를 향하게 만들기
public class BossBullet1 : MonoBehaviour
{
    // 속도
    public float speed = 3;
    // 플레이어
    GameObject player;
    // 방향
    Vector3 dir;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기 
        player = GameObject.Find("Player");
        dir = player.transform.position - transform.position;
        transform.forward = dir;
        dir.Normalize();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.3409f * 5)
        {
            // 움직이기
            transform.position += speed * dir * Time.deltaTime;
        }
    }

    // 플레이어 충돌시 죽기
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor") || other.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            Destroy(gameObject);
        }
    }
}
