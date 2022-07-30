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
        Move,
        Stop
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
        else if (batState == BatState.Stop)
        {
            BatStop();
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
    Vector3 batDir;
    Vector3 pos;

    // �÷��̾�� �����ٸ� ���߿��� �������� ������
    private void BatMove()
    {

        currentTime += Time.deltaTime;
        if (currentTime > moveTime)
        {
            x = UnityEngine.Random.Range(-1, 1);
            y = UnityEngine.Random.Range(2, 5);
            z = UnityEngine.Random.Range(-2, 2);
            pos = new Vector3(x, y, z);
            batDir = pos - transform.position;
            currentTime = 0;
        }
        print(pos);

        transform.position += batDir.normalized * bSpeed * Time.deltaTime;
        float dis = Vector3.Distance(pos, transform.position);
        if(dis < 0.5f)
        {
            batState = BatState.Stop;
        }
    }
    void BatStop()
    {
        transform.position += dir * 0 * Time.deltaTime;
    }

}
