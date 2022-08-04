using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Potion : MonoBehaviour
{
    Transform player;
    Vector3 dis;

    public float senseDis = 3;

    private void Update()
    {
        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        SR_PlayerHP playerHP = player.GetComponent<SR_PlayerHP>();

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (playerHP != null)
            {
                if (Input.GetKeyDown(KeyCode.F) && wallet > 2)
                {
                    if (playerHP.hp < 100)
                    {
                        playerHP.AddHP();
                        Destroy(gameObject);
                        PlayerPrefs.SetInt("Wallet", wallet - 2);

                    }
                }
            }
        }
    }

}