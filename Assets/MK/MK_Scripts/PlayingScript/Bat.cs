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
    public float change = 0.2f;

    // ���� �ӽ�
    enum BatState
    {
        Come,
        Move,
        Stop,
        Back,
        Die
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
    float currentTime3;

    // ���� ��ǥ
    float x;
    float y;
    float z;
    // ���� ���� �� ��ġ
    Vector3 batDir;
    Vector3 pos;
    BatHP bat;

    BoxCollider collider;

    // ������ٵ�
    Rigidbody rigid;
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        // �Ѿ˹߻��ϱ�
        currentTime2 += Time.deltaTime;

    }
    // Start is called before the first frame update
    void Start()
    {
        // �� ü�� ����
        bat = GetComponent<BatHP>();
        bat.ENEMYHP = 1;
        float y = UnityEngine.Random.Range(6, 10);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        // �÷��̾��
        Vector3 mySight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(mySight);

        float rnd = UnityEngine.Random.Range(5, 8);
        // �÷��̾ ���ϴ� ����
        dir = player.transform.position - transform.position;
        dir = new Vector3(dir.x, dir.y + rnd, dir.z);
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
        else if(batState == BatState.Stop)
        {
            BatStop();
        }
        else if (batState == BatState.Back)
        {
            BatBack();
        }
        else if (batState == BatState.Die)
        {
            transform.position += dir * 0 * bSpeed * Time.deltaTime;
        }

        if (bat.ENEMYHP > 0)
        {
            if (currentTime2 > shootTime * 0.3375f)
            {
                GameObject bullet = Instantiate(bulletFact);
                bullet.transform.position = transform.position;
                currentTime2 = 0;
            }
        }
        else
        {
            batState = BatState.Die;
            collider.enabled = false;
        }
    }
    // �÷��̾ ���� Ư�� �κб��� �������
    private void BatCome()
    {
        transform.position += dir * bSpeed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis < pDis)
        {
            batState = BatState.Stop;
        }

    }
    int time;
    // �÷��̾�� �����ٸ� ���߿��� �������� ������
    private void BatMove()
    {
        time++;
        if(time < 1)
        {
            x = UnityEngine.Random.Range(-10, 10);
            y = UnityEngine.Random.Range(4, 7);
            z = UnityEngine.Random.Range(-10, 10);
        }
        pos = new Vector3(x, y + 2, z);
        batDir = pos - transform.position;
        batDir.Normalize();
        transform.position += batDir * bSpeed * Time.deltaTime;

        float dis = Vector3.Distance(transform.position, pos);
        if(dis < 0.1f)
        {
            time = 0;
            batState = BatState.Stop;
        }
    }

    void BatStop()
    {
        transform.position += batDir * 0 * Time.deltaTime;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis < 3)
        {
            batState = BatState.Back;
        }
        else if(dis >= 3 && dis < pDis)
        {
            batState = BatState.Move;
        }
        else if(dis >= pDis)
        {
            batState = BatState.Come;
        }
    }
    // ������ �ڷ� ����
    void BatBack()
    {
        currentTime3 += Time.deltaTime;
        transform.position += -dir * bSpeed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis >= pDis)
        {
            currentTime = 0;
            batState = BatState.Move;
        }
    }

    // ���� �ε����� ���
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            rigid.velocity = new Vector3(0, 0, 0);
            batState = BatState.Stop;
        }
    }
}
