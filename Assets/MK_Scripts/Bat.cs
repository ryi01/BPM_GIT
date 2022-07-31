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
    // �Ѿ� �߻�ð�
    public float shootTime = 3;
    // �Ѿ� ����
    public GameObject bulletFact;

    // ���� �ӽ�
    enum BatState
    {
        Come,
        Move,
    }
    BatState batState;
    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // �ð�
    float currentTime;
    // �Ѿ� �߻�� �ð�
    float currentTime2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Dummy_Player");

        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);

        // �÷��̾ ���ϴ� ����
        dir = player.transform.position - transform.position;
        dir.Normalize();
        // FSM
        if(batState == BatState.Move)
        {
            BatMove();
        }
        else if(batState == BatState.Come)
        {
            BatCome();
        }

        // �Ѿ˹߻��ϱ�
        currentTime2 += Time.deltaTime;
        if(currentTime2 > shootTime)
        {
            GameObject bullet = Instantiate(bulletFact);
            bullet.transform.position = transform.position;
            currentTime2 = 0;
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
            // �÷��̾ ���� �����̱�
            transform.position += dir * bSpeed * Time.deltaTime;
        }
        // �÷��̾�� ������
        else
        {
            // ������Ʈ �ٲٱ�
            batState = BatState.Move;
        }
    }
    // ���� ��ǥ
    float x;
    float y;
    float z;
    // ���� ���� �� ��ġ
    Vector3 batDir;
    Vector3 pos;

    // �÷��̾�� �����ٸ� ���߿��� �������� ������
    private void BatMove()
    {
        currentTime += Time.deltaTime;
        // ����ð��� 4�ʺ��� ũ�� 5�ʺ��� ���� ��
        if (currentTime > 4 && currentTime < 5)
        {
            // ���� ��ǥ ���ϰ�
            x = UnityEngine.Random.Range(-4, 4);
            y = UnityEngine.Random.Range(1, 4);
            z = UnityEngine.Random.Range(-4, 4);
            // �÷��̾� ��ó�� ����
            pos = player.transform.position + new Vector3(x, y, z);
            // ���� ��ġ ���� ����
            batDir = pos - transform.position;
            currentTime = 0;
        }
        // ���� ������ġ�� �ִٸ�
        float dis = Vector3.Distance(transform.position, pos);
        if (dis > 0.1f)
        {
            // �����̱�
            transform.position += batDir.normalized * bSpeed * Time.deltaTime;
        }
        
    }

}
