using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopRifle : MonoBehaviour
{
    public int k = 0;
    public int k1 = 0;

    GameObject player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    int nCoin;

    private void Update()
    {
        player = GameObject.Find("Player");
        nCoin = player.GetComponent<SR_PlayerInventory>().numberOfCoins;
        print(nCoin);

        dis = player.transform.position - transform.position;

        //int wallet = PlayerPrefs.GetInt("Wallet");
        if (dis.magnitude <= senseDis)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (nCoin >= 5 && cnt <= 0)
                {
                    // ShotGun 으로 바꾼다
                    //PlayerPrefs.SetInt("Wallet", wallet - 5);
                    nCoin -= 5;
                    player.GetComponent<SR_PlayerInventory>().numberOfCoins = nCoin;
                    cnt++;
                    gameObject.SetActive(false);
                }

                // 부모 객체의 이름에 따라 증가하는 k값이 다름
                if (gameObject.transform.parent.name == "Gun Box 1" && cnt>0)
                {
                    k++;
                }
                if (gameObject.transform.parent.name == "Gun Box 2" && cnt>0)
                {
                    k1++;
                }
            }

        }
    }

}
