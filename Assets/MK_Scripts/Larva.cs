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

    // �÷��̾� ��ġ
    Transform player;
    // ������ٵ�
    Rigidbody rigid;
    // ����
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Pos").transform;
        // ������ٵ� ��������
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mySight = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(mySight);

        // ���� ����
        dir = player.position - transform.position;
        dir.Normalize();

        // �÷��̾� ���ϱ�
        transform.position += dir * speed * Time.deltaTime;
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
        if (collision.gameObject.name.Contains("Floor"))
        {
            rigid.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        }
        if (collision.gameObject.name.Contains("Player"))
        {
            NockBack();
        }
    }
}
