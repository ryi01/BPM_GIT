using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ź� : ���� �ð����� �÷��̾��� ��ġ�� �����ϰų� ������
// FSM ����ؼ� Move, Jump, Run���� �ٲ���ҵ�
public class Spider : MonoBehaviour
{
    // �ʿ�Ӽ� : ���� �ð�
    float currentTime = 0;
    // �ӵ�
    float speed = 4;
    // �÷��̾�
    Transform player;
    // ����
    Vector3 dir;
    // ���ߴ� �ð�
    float stopTime = 3;

    enum SpiderState
    {
        Move,
        Run,
        Jump,
        Stop
    }
    SpiderState state;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Dummy_Player").transform;
        state = SpiderState.Move;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        dir = player.transform.position - transform.position;

        if (state == SpiderState.Move)
        {
            SpidexrMove();
        }
        else if (state == SpiderState.Stop)
        {
            SpiderStop();
        }
        else if(state == SpiderState.Run)
        {
            SpiderRun();
        }
        else if (state == SpiderState.Jump)
        {
            SpiderJump();
        }

    }

    // �⺻���� ������
    // �÷��̾������� ���ϴٰ� ���� �ð��� Stop���� �ٲ��
    private void SpidexrMove()
    {
        // ������
        transform.position += dir * speed * Time.deltaTime;

        // ���� �ð��� �Ǹ�
        currentTime += Time.deltaTime;
        int rndTime = Random.Range(2, 5);
        if (currentTime > rndTime)
        {
            // stop���� �ٲ��
            state = SpiderState.Stop;
            currentTime = 0;
        }

    }

    // �� �ڸ��� ���� ���߰� �÷��̾ �ٶ󺸴ٰ� ���� �ð��� ���� ��, ���̳� ���� ���� �ٲ�
    private void SpiderStop()
    {
        Debug.Log("Stop");
        // �� �ڸ��� ��������, �÷��̾�� �ٶ������.
        transform.position += dir * 0 * Time.deltaTime;
        // �����ð��� ������
        currentTime += Time.deltaTime;
        if(currentTime > stopTime)
        {
            int rndAttack = Random.Range(0, 1);
            if(rndAttack == 0)
            {
                state = SpiderState.Run;
            }
            else
            {
                state = SpiderState.Jump;
            }
        }
        
    }
    private void SpiderRun()
    {
        Debug.Log("RUN");
    }

    private void SpiderJump()
    {
        Debug.Log("Jump");
    }

 /*   void SpiderAttack(Vector3 dir)
    {        
        // �������� �� �ൿ�� ����
        int rnd = Random.Range(0, 1);
        if (rnd == 0)
        {
            // �����ð�����
            float rndTime = Random.Range(3, 10);
            // �ð��� �帣��
            currentTime += Time.deltaTime;
            if (rndTime < currentTime)
            {
                // �������� �����ϰ� �ʹ�
                transform.position = Vector3.Slerp(transform.position, player.transform.position, 0.05f);
            }
        }
        else
        {
            // �����ð�����
            float rndTime = Random.Range(3, 10);
            // �ð��� �帣��
            currentTime += Time.deltaTime;
            if (rndTime < currentTime)
            {
                // �������� �����ϰ� �ʹ�
                transform.position += dir * speed * 2 * Time.deltaTime;
            }
        }

    }*/
}
