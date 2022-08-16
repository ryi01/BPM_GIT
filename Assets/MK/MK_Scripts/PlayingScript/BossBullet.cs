using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ӵ��� �÷��̾ ���ϰ� �����
public class BossBullet : MonoBehaviour
{
    // �ӵ�
    public float speed = 3;
    // �÷��̾�
    GameObject player;
    // ����
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ã�� 
        player = GameObject.Find("Player");
        dir = player.transform.position - transform.position;
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        // �����̱�
        transform.position += speed * dir * Time.deltaTime;
    }

    // �÷��̾� �浹�� �ױ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            Destroy(gameObject);
        }
    }
}
