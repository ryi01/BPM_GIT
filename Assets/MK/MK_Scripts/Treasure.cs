using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보물 상자 : 적이 모두 죽으면 setactive되기
public class Treasure : MonoBehaviour
{
    // 필요 속성 : 적, 보물 상자
    public GameObject treasure;
    // 필요속성 : 키공장
    public GameObject keyFact;
    // 카운트
    public int count;
    GameObject[] enemy;
    int countKey;
    GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        // 보물 상자 off
        treasure.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // 적 태그를 가진 오브젝트 찾기
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        // 태그가 없으면 보물 on
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
