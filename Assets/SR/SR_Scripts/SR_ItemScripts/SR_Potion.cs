using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Potion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SR_PlayerHP playerHP = other.GetComponent<SR_PlayerHP>();

        if (playerHP != null)
        {
            if(playerHP.hp<100)
            {
                playerHP.AddHP();
                gameObject.SetActive(false);
            }
        }
    }
}
