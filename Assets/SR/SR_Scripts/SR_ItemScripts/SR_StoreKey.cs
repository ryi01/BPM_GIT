using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_StoreKey : MonoBehaviour
{
    Transform player;
    Vector3 dis;

    public float senseDis = 3;

    private void Update()
    {
        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        SR_PlayerInventory playerInventory = player.GetComponent<SR_PlayerInventory>();

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (playerInventory != null)
            {
                if (Input.GetKeyDown(KeyCode.F) && wallet > 2)
                {
                    playerInventory.KeyCollected();
                    PlayerPrefs.SetInt("Wallet", wallet - 2);
                    Destroy(gameObject);


                }
            }
        }
    }
}
