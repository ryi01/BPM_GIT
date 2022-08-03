using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Scroll : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        int skill = PlayerPrefs.GetInt("Skill");

        SR_PlayerInventory playerInventory = other.GetComponent<SR_PlayerInventory>();

        if(playerInventory != null)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                playerInventory.ScrollCollected();
                Destroy(gameObject);
                PlayerPrefs.SetInt("Skill", 1);

            }
        }
    }
}
