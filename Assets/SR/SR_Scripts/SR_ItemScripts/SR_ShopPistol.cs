using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopPistol : MonoBehaviour
{
    public int k = 0;
    public int k1 = 0;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;
    private void Update()
    {

        player = GameObject.Find("Player").transform;

        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - transform.position;
        if (dis.magnitude <= senseDis)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (/* && wallet > 15 && */ cnt <= 0)
                {
                    // ShotGun ���� �ٲ۴�
                    PlayerPrefs.SetInt("Wallet", wallet - 15);
                    cnt++;
                }

                gameObject.SetActive(false);
                // �θ� ��ü�� �̸��� ���� �����ϴ� k���� �ٸ�
                if (gameObject.transform.parent.name == "Gun Box 1")
                {
                    k++;
                }           
                if(gameObject.transform.parent.name == "Gun Box 2")
                {
                    k1++;
                }
                
            }
        }
    }
}
