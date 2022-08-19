using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_StoreKey : MonoBehaviour
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

        SR_PlayerInventory playerInventory = player.GetComponent<SR_PlayerInventory>();

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (playerInventory != null)
            {
                if (Input.GetKeyDown(KeyCode.F) && nCoin >= 2)
                {
                    playerInventory.KeyCollected();
                    nCoin -= 2;
                    player.GetComponent<SR_PlayerInventory>().numberOfCoins = nCoin;
                    //PlayerPrefs.SetInt("Wallet", wallet - 2);
                    Destroy(gameObject);


                }
            }
        }
    }
}
