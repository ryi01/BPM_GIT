using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� : �÷��̾�� �ָ� �ٰ�����, ���� �Ÿ����� ������ �߻�, �����Ÿ� ���� �������� ������
public class Bat : MonoBehaviour
{
    // �÷��̾���� �Ÿ�
    public float pDis = 4;
    // ���� �ӵ�
    public float bSpeed = 4;
    // �����̴� �ð�
    public float moveTime = 2;
    // ��
    public static Vector3 insideUnitSpehre;

    // ���� �ӽ�
    enum BatState
    {
        Come,
        Move
    }
    BatState batState;
    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // �ð�
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Dummy_Player");
        dir = player.transform.position - transform.position;
        dir.Normalize();

        if(batState == BatState.Move)
        {
            BatMove();
        }
        else if(batState == BatState.Come)
        {
            BatCome();
        }
    }
    // �÷��̾ ���� Ư�� �κб��� �������
    private void BatCome()
    {
        // �÷��̾���� ���� ���
        float dis = Vector3.Distance(player.transform.position, transform.position);
        // �÷��̾�� �ִٸ�
        if(dis > pDis)
        {
            transform.position += dir * bSpeed * Time.deltaTime;
        }
        else
        {
            batState = BatState.Move;
        }
    }
    float x;
    float y;
    float z;

    // �÷��̾�� �����ٸ� ���߿��� �������� ������
    private void BatMove()
    {
        currentTime += Time.deltaTime;
        if (currentTime > moveTime)
        {
            x = UnityEngine.Random.Range(-2, 2);
            y = UnityEngine.Random.Range(2, 5);
            z = UnityEngine.Random.Range(-2, 2);
        }
        Vector3 pos = new Vector3(x, y, z);
        Vector3 dir = pos - transform.position;
        transform.position += dir * bSpeed * Time.deltaTime;

        
        if(transform.position )

        
    }

}
