using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopShotGun : MonoBehaviour
{
    public int k = 0;
    public int k1 = 0;

    Transform player;

    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    int nCoin;

    private void Update()
    {

        player = GameObject.Find("Player").transform;
        nCoin = player.GetComponent<SR_PlayerInventory>().numberOfCoins;

        dis = player.position - transform.position;

        //int wallet = PlayerPrefs.GetInt("Wallet");

        if (dis.magnitude <= senseDis)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (nCoin >= 15 && cnt <= 0)
                {
                    // ShotGun ���� �ٲ۴�
                    //PlayerPrefs.SetInt("Wallet", nCoin - 15);
                    nCoin -= 15;
                    player.GetComponent<SR_PlayerInventory>().numberOfCoins = nCoin;
                    cnt++;
                }

                gameObject.SetActive(false);
                // �θ� ��ü�� �̸��� ���� �����ϴ� k���� �ٸ�
                if (gameObject.transform.parent.name == "Gun Box 1")
                {
                    k++;
                }
                if (gameObject.transform.parent.name == "Gun Box 2")
                {
                    k1++;
                }
            }
        }
    }

}
