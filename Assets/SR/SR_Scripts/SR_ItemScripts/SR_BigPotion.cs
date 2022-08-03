using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_BigPotion : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        int wallet = PlayerPrefs.GetInt("Wallet");

        SR_PlayerHP playerHP = other.GetComponent<SR_PlayerHP>();

        if (playerHP != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && wallet > 6)
            {
                if (playerHP.hp < 100)
                {
                    playerHP.AddBigHP();
                    Destroy(gameObject);
                    PlayerPrefs.SetInt("Wallet", wallet - 6);

                }
            }
        }
    }
}