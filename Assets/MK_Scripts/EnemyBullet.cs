using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ���� ����!
public class EnemyBullet : MonoBehaviour
{
    // �ӵ�
    public float bulletSpeed = 4;
    // ����
    Vector3 dir;
    // �÷��̾�
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player");
        // ����
        dir = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ ����
        transform.LookAt(player.transform.position);
        // �����̱�
        transform.position += dir * bulletSpeed * Time.deltaTime;
    }
}
