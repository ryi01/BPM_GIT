using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코인 자전시키기
public class JumpItem1 : MonoBehaviour
{
    // 보물상자
    GameObject tre;
    // 속도
    public float speed = 5;
    // 점프 파워
    public float jumpPow = 5;
    // 방향
    Vector3 dir;

    private void Start()
    {
        tre = GameObject.Find("Treasure");
        CreateCoin();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= tre.transform.position.y + 0.18f)
        {
            transform.position = new Vector3(transform.position.x, tre.transform.position.y + 0.18f, transform.position.z);
        }
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    float currentTime;
    float x;
    float z;
    // 생성 되자마자
    void CreateCoin()
    {    
        Vector3 pos = transform.position + new Vector3(x + 0.04f, 1.8f, z + 0.05f);
        dir = pos - transform.position;

        dir.y = jumpPow;

        transform.position += dir * speed * Time.deltaTime;
    }
}
