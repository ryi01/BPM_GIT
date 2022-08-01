using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코인 자전시키기
public class Coin : MonoBehaviour
{
    // 속도
    public float speed = 5;
    // 점프 파워
    public float jumpPow = 5;
    // 방향
    Vector3 dir;


    private void Awake()
    {
        CreateCoin();
    }

    private void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 1.4f)
        {
            transform.position = new Vector3(transform.position.x, 1.4f, transform.position.z);
        }
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    // 생성 되자마자
    void CreateCoin()
    {
        float x = Random.Range(-2, 2);
        float y = Random.Range(2, 4);
        float z = Random.Range(-2, 2);

        Vector3 pos = transform.position + new Vector3(x, y, z);
        dir = pos - transform.position;

        dir.y = jumpPow;

        transform.position += dir * speed * Time.deltaTime;
    }
}
