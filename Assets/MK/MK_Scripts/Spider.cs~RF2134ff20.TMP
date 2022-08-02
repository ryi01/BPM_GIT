using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ź� : ���� �ð����� �÷��̾��� ��ġ�� �����ϰų� ������
// FSM �����ؼ� Move, Jump, Run���� �ٲ����ҵ�
// ���ݺ� �߰��ϱ� 07.29 ���� ���� => �ϸ� ������
public class Spider : MonoBehaviour
{
    // ���� ��
    public float jumpPow = 2;
    // �ʿ��Ӽ� : ���� �ð�
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
    // �÷��̾����� ����
    Vector3 runDir;
    // y�ӵ�
    float yVelocity;
    // �������ٵ�
    Rigidbody sRigid;
    // �÷��̾����� �Ÿ�
    float dis;

    enum SpiderState
    {
        Move,
        Run,
        Jump,
        Stop,
        Set,
        Attack
    }
    SpiderState state;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player").GetComponent<Transform>();
        state = SpiderState.Move;
        sRigid = GetComponent<Rigidbody>();

        // �� ü�� ����
        SpiderHP.instance.ENEMYHP = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �÷��̾������� ����
        dir = player.position - transform.position;
        dir.Normalize();
        // �߷� �ֱ�
        yVelocity += -9.8f * Time.deltaTime;

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
        else if (state == SpiderState.Set)
        {
            SpiderSet();
        }
        else if(state == SpiderState.Attack)
        {
            SpiderAttack();
        }

        if (state == SpiderState.Jump)
        {
            yVelocity = jumpPow;
        }

    }

    private void Update()
    {
        dis = Vector3.Distance(player.position, transform.position);
        if (dis < 1)
        {
            Debug.Log("Attack");
            state = SpiderState.Attack;
        }
    }

    // �⺻���� ������
    // �÷��̾������� ���ϴٰ� ���� �ð��� Stop���� �ٲ���
    private void SpidexrMove()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);
        // ������
        transform.position += dir * speed * Time.deltaTime;

        // ���� �ð��� �Ǹ�
        currentTime += Time.deltaTime;
        int rndTime = Random.Range(2, 8);
        if (currentTime > rndTime)
        {
            // stop���� �ٲ���
            state = SpiderState.Stop;
            currentTime = 0;
        }

    }

    // �� �ڸ��� ���� ���߰� �÷��̾ �ٶ󺸴ٰ� ���� �ð��� ���� ��, ���̳� ���� ������ �ٲ�
    private void SpiderStop()
    {
        LookPlayer();
        // �����ð��� ������
        currentTime += Time.deltaTime;
        if(currentTime > stopTime)
        {
            // �÷��̾� ����
            runDir = player.position - transform.position;
            runDir.Normalize();

            int rndAttack = Random.Range(0, 2);
            if (rndAttack == 0)
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
        // �����ð����� ���� �޸� => �÷��̾��� ��ġ�� �ٴ� ���� ����
        currentTime += Time.deltaTime;

        transform.position += runDir * (speed + 3f) * Time.deltaTime;
        if (currentTime >= runTime)
        {
            currentTime = 0;
            state = SpiderState.Set;
        }
    }
    private void SpiderJump()
    {

        // Ư���������� �����ؾ���
        runDir.y = yVelocity;

        transform.position += runDir * speed * Time.deltaTime;

    }

    // ������ ���� �÷��̾� �ٶ󺸱�
    private void SpiderSet()
    {
        LookPlayer();

        currentTime += Time.deltaTime;
        if(currentTime > 3)
        {
            state = SpiderState.Move;
            currentTime = 0;
        }
    }
    // ���� �����ϱ�
    private void SpiderAttack()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 1)
        {
            Debug.Log("Attack");
            currentTime = 0;
        }

        if(dis >= 1)
        {
            state = SpiderState.Move;
        }

    }
    void LookPlayer()
    {
        // �÷��̾� ����
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // �� �ڸ��� ��������, �÷��̾��� �ٶ�������.
        transform.position += dir * 0 * Time.deltaTime;
    }


    // �ε����� ����,
    // �˹� ��
    public float backPow = 3;
    // �˹��� �Լ�
    public void NockBack()
    {
        sRigid.AddForce(-dir * backPow, ForceMode.Impulse);
        state = SpiderState.Set;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Contains("Plane") && state == SpiderState.Jump)
        {
            // set���·� ����
            state = SpiderState.Set;
        }
    }

}
