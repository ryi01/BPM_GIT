using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������Ű��
public class CoinRot : MonoBehaviour
{
    // �ӵ�
    public float speed = 5;
    // ���� �Ŀ�
    public float jumpPow = 5;
    // ����
    Vector3 dir;

    private void Start()
    {
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
            x = Random.Range(-3, 3);
            y = Random.Range(4, 10);
            z = Random.Range(-3, 3);
            currentTime = 0;
        }        
        Vector3 pos = transform.position + new Vector3(x, y, z);
        dir = pos - transform.position;

        dir.y = jumpPow;

        transform.position += dir * speed * Time.deltaTime;
    }
}
