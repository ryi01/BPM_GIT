using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������Ű��
// ���� y���� 0.8���� ���̳� => ���ϴ� ��ġ + 0.8
public class PortionRot : MonoBehaviour
{
    // �ӵ�
    public float speed = 5;
    // ���ϴ� y��
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
