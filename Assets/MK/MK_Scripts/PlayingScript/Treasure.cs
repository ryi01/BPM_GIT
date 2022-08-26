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
    public GameObject enemy2notClear;

    public GameObject enemy1ToStart;

    public GameObject enemy2ToEnemy1;

    GameObject[] enemy;
    int countKey;
    GameObject key;

    int clearCount = 0;

    public GameObject effectFactory;

    public GameObject closedBox;
    public GameObject openedBox;

    AudioSource audio;

    int h = 0;
    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� off
        treasure.gameObject.SetActive(false);
        audio = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �� �±׸� ���� ������Ʈ ã��
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject room = GameObject.Find("EnemyManager");
        int a = enemy2ToEnemy1.GetComponent<SR_Enemy2ToEnemy1>().clearEnemy2;

        // �±װ� ������ ���� on
        if (enemy.Length == 0 && h==0)
        {
            treasure.gameObject.SetActive(true);

            for(int i=0;i<3;i++) Instantiate(effectFactory, transform);



            if (a <= 0)
            {
                notClear.SetActive(false);
                clear.SetActive(true);
                closedBox.SetActive(true);
                //openedBox.SetActive(false);
            }
            else
            {
                notClear.SetActive(false);
                clear.SetActive(false);
                enemy2notClear.SetActive(false);

            }
            h++;
        }
        GameObject player = GameObject.Find("Player");
        float dis = Vector3.Distance(treasure.transform.position, player.transform.position);
        //print(dis);
        if(treasure.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.F) && count < 1 && dis < 5)
        {
            key = Instantiate(keyFact);
            key.transform.position = transform.position;
            count++;
            for (int i = 0; i < 3; i++) Instantiate(effectFactory, transform);

            closedBox.SetActive(false);
            openedBox.SetActive(true);




        }

    }


}
