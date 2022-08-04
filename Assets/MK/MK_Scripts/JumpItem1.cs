using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������Ű��
public class JumpItem1 : MonoBehaviour
{
    // ��������
    GameObject tre;
    // �ӵ�
    public float speed = 5;
    // ���� �Ŀ�
    public float jumpPow = 5;
    // ����
    Vector3 dir;

    private void Start()
    {
        tre = GameObject.Find("Treasure");
        CreateCoin();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 1.4f)
        {
            transform.position = new Vector3(transform.position.x, 1.4f, transform.position.z);
        }
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    float currentTime;
    float x;
    float y;
    float z;
    // ���� ���ڸ���
    void CreateCoin()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.005f)
        {
            x = Random.Range(-1, 1);
            z = Random.Range(-1, 1);
            currentTime = 0;
        }        
        Vector3 pos = tre.transform.position + new Vector3(x, 1.8f, z);
        dir = pos - transform.position;

        dir.y = jumpPow;

        transform.position += dir * speed * Time.deltaTime;
    }
}
