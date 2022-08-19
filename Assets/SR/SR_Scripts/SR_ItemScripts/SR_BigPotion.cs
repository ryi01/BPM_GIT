using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BigPotion : MonoBehaviour
{
    Transform player;
    Vector3 dis;

    public float senseDis = 3;

    int nCoin;

    private void Update()
    {
        player = GameObject.Find("Player").transform;
        //int wallet = PlayerPrefs.GetInt("Wallet");
        nCoin = player.GetComponent<SR_PlayerInventory>().numberOfCoins;

        SR_PlayerHP playerHP = player.GetComponent<SR_PlayerHP>();

        dis = player.position - gameObject.transform.position;
        if(dis.magnitude <= senseDis)
        {
            if (playerHP != null)
            {
                if (Input.GetKeyDown(KeyCode.F) && nCoin >= 4)
                {
                    if (playerHP.hp < 100)
                    {
                        playerHP.AddBigHP();
                        Destroy(gameObject);
                        nCoin -= 4;
                        player.GetComponent<SR_PlayerInventory>().numberOfCoins = nCoin;
                        //PlayerPrefs.SetInt("Wallet", wallet - 4);

                    }
                }
            }
        }
    }
    
}