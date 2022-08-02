using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SR_PlayerInventory playerInventory = other.GetComponent<SR_PlayerInventory>();

        if(playerInventory != null)
        {
            playerInventory.CoinCollected();
            Destroy(gameObject);
        }
    }
}
