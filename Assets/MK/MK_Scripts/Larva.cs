using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ֹ��� : ������ ��� �����ϸ鼭 �÷��̾ ����ٴ�
public class Larva : MonoBehaviour
{
    // �ֹ��� �ӵ�
    public float speed = 3.0f;
    // ���� ��
    public float jumpPow = 10;
    // ���� �ϴ� �ð�
    public float jumpTime = 1;
    // �÷��̾���� �Ÿ�
    public float moveDis = 2;
    // �˹� ��
    public float backPow = 3;
    // ���� �ð�
    public float attackTime = 2;

    // FSM
    enum LarvaState
    {
        Move,
        Attack
    }
    LarvaState state;
    // �÷��̾� ��ġ
    Transform player;
    // ������ٵ�
    Rigidbody rigid;
    // ����
    Vector3 dir;
    // �ð�
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player").transform;
        // ������ٵ� ��������
        rigid = GetComponent<Rigidbody>();
        // ü��
        // �� ü�� ����
        LarvaHP hp = GetComponent<LarvaHP>();
        hp.ENEMYHP = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == LarvaState.Move)
        {
            LarvaMove();
        }
        else if(state == LarvaState.Attack)
        {
            LarvaAttack();
        }

    }
    // �÷��̾ ���� ������
    void LarvaMove()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // ���� ����
        dir = player.position - transform.position;
        dir.Normalize();

        // �÷��̾� ���ϱ�
        transform.position += dir * speed * Time.deltaTime;
        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis < 2)
        {
            state = LarvaState.Attack;
        }
    }
    // �÷��̾� ����
    void LarvaAttack()
    {
        currentTime += Time.deltaTime;
        if(currentTime > attackTime)
        {
            player.GetComponent<SR_PlayerHP>().hp -= 25;
            state = LarvaState.Move;
            currentTime = 0;
        }
    }

    // �˹�� �Լ�
    public void NockBack()
    {
        rigid.AddForce(-dir * backPow, ForceMode.Impulse);
    }

    // ���� ������ ����
    // �÷��̾�� ������ ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Plane"))
        {
            rigid.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        }
    }
}
