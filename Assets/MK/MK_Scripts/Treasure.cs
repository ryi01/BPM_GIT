using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� : ���� ��� ������ setactive�Ǳ�
public class Treasure : MonoBehaviour
{
    // �ʿ� �Ӽ� : ��, ���� ����
    public GameObject treasure;
    // �ʿ�Ӽ� : Ű����
    public GameObject keyFact;
    // ī��Ʈ
    public int count;
    GameObject[] enemy;
    int countKey;
    GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� off
        treasure.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // �� �±׸� ���� ������Ʈ ã��
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        // �±װ� ������ ���� on
        if(enemy.Length == 0)
        {
            treasure.gameObject.SetActive(true);
        }
        GameObject player = GameObject.Find("Player");
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(treasure.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.F) && countKey < 1 && dis < 5)
        {
            countKey++;
            key = Instantiate(keyFact);
            key.transform.position = transform.position + new Vector3(0, 0f, 0);

        }

    }
}
