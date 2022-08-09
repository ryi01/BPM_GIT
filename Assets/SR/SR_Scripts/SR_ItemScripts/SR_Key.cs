using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Key : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {

        SR_PlayerInventory playerInventory = other.GetComponent<SR_PlayerInventory>();
        if (playerInventory != null)
        {
            
             playerInventory.KeyCollected();
             Destroy(gameObject);


        }
    }
}
