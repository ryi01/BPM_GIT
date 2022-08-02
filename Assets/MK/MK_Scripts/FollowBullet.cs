using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ���� �ٴϱ�
public class FollowBullet : MonoBehaviour
{
    // �ӵ� 
    public float speed = 20;
    // ����
    Vector3 dir;
    // �÷��̾�
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // ����
        dir = player.transform.position - transform.position;
        dir.Normalize();

        // �����̱�
        transform.position += speed * dir * Time.deltaTime;
    }
    // �÷��̾� �浹�� �ױ�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            Destroy(gameObject);
        }
    }
}
