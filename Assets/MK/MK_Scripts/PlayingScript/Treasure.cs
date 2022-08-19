using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� : ���� ���� ������ setactive�Ǳ�
public class Treasure : MonoBehaviour
{
    // �ʿ� �Ӽ� : ��, ���� ����
    public GameObject treasure;
    // �ʿ��Ӽ� : Ű����
    public GameObject keyFact;
    // ī��Ʈ
    public int count;

    public GameObject notClear;
    public GameObject clear;

    public GameObject enemy1ToStart;

    GameObject[] enemy;
    int countKey;
    GameObject key;

    int clearCount = 0;
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
        if (enemy.Length == 0)
        {
            treasure.gameObject.SetActive(true);
            notClear.SetActive(false);
            clear.SetActive(true);
        }
        GameObject player = GameObject.Find("Player");
        float dis = Vector3.Distance(treasure.transform.position, player.transform.position);
        //print(dis);
        if(treasure.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.F) && count < 1 && dis < 5)
        {
            key = Instantiate(keyFact);
            key.transform.position = transform.position;
            count++;


        }

    }
}
