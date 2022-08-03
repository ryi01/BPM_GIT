using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Key : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        int wallet = PlayerPrefs.GetInt("Wallet");

        SR_PlayerInventory playerInventory = other.GetComponent<SR_PlayerInventory>();
        if (playerInventory != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && wallet > 2)
            {
                playerInventory.KeyCollected();
                Destroy(gameObject);
                PlayerPrefs.SetInt("Wallet", wallet-2);


            }
        }
    }
}
