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
        if(enemy.Length == count)
        {
            treasure.gameObject.SetActive(true);
        }

        if(treasure.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.F))
        {
            GameObject key = Instantiate(keyFact, gameObject.transform);
           //  key.transform.position = treasure.transform.position;
        }
    }
}
