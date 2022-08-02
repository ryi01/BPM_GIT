using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 열쇠 자전시키기
// 열쇠 y축이 0.8정도 차이남 => 원하는 위치 + 0.8
public class PortionRot : MonoBehaviour
{
    // 속도
    public float speed = 5;
    // 원하는 y축
    public float y = 2f;

    private void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        float hight = y + 0.8f;
        transform.position = new Vector3(transform.position.x, hight, transform.position.z);
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }

}
