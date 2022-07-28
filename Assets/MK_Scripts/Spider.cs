using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ź� : ���� �ð����� �÷��̾��� ��ġ�� �����ϰų� ������
// FSM ����ؼ� Move, Jump, Run���� �ٲ���ҵ�
public class Spider : MonoBehaviour
{
    // ���� ��
    public float jumpPow = 2;
    // �ʿ�Ӽ� : ���� �ð�
    float currentTime = 0;
    // �ӵ�
    float speed = 4;
    // �÷��̾�
    Transform player;
    // ����
    Vector3 dir;
    // ���ߴ� �ð�
    float stopTime = 2;
    // �޸��� �ð�
    float runTime = 4;
    // �÷��̾���� ����
    Vector3 runDir;
    // �÷��̾��� ��ġ
    Vector3 pPos;
    // ������ٵ�
    Rigidbody sRigid;

    enum SpiderState
    {
        Move,
        Run,
        Jump,
        Stop,
        Set
    }
    SpiderState state;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Dummy_Player").transform;
        state = SpiderState.Move;
        sRigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        dir = player.transform.position - transform.position;
        dir.Normalize();

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
        else if(state == SpiderState.Set)
        {
            SpiderSet();
        }

    }

    // �⺻���� ������
    // �÷��̾������� ���ϴٰ� ���� �ð��� Stop���� �ٲ��
    private void SpidexrMove()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);
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
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // �� �ڸ��� ��������, �÷��̾�� �ٶ������.
        transform.position += dir * 0 * Time.deltaTime;
        // �����ð��� ������
        currentTime += Time.deltaTime;
        if(currentTime > stopTime)
        {
            // �÷��̾� ����
            runDir = player.position - transform.position;
            runDir.Normalize();
            state = SpiderState.Jump;
            print(pPos);
            /*int rndAttack = Random.Range(0, 1);
            if(rndAttack == 0)
            {
                state = SpiderState.Run;
            }
            else
            {
                state = SpiderState.Jump;
            }*/
        }
        
    }
    private void SpiderRun()
    {

        // �����ð����� ��� �޸� => �÷��̾��� ��ġ�� �ٴ� ���� ����
        currentTime += Time.deltaTime;

        transform.position += runDir * (speed + 1f) * Time.deltaTime;
        if (currentTime >= runTime)
        {
            currentTime = 0;
            state = SpiderState.Set;
        }
    }

    private void SpiderJump()
    {
        runDir += Vector3.up;
        // �������� �����ϰ� �ʹ�
        sRigid.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        float dis = Vector3.Distance(transform.position, runDir);
        if(dis < 2)
        {
            state = SpiderState.Set;
        }
    }
    // ������ ���� �÷��̾� �ٶ󺸱�
    private void SpiderSet()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // �� �ڸ��� ��������, �÷��̾�� �ٶ������.
        transform.position += dir * 0 * Time.deltaTime;

        currentTime += Time.deltaTime;
        if(currentTime > 3)
        {
            state = SpiderState.Move;
            currentTime = 0;
        }
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
