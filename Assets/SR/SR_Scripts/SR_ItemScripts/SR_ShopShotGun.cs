using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopShotGun : MonoBehaviour
{
    public int k = 0;
    SR_ShopRifle reRifle;
    private void Start()
    {
        reRifle = GameObject.Find("ShopRifle").GetComponent<SR_ShopRifle>();
    }
    private void Update()
    {
        if (reRifle)
        {
            if (reRifle.k == 1) reRifle.k = 0;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            int wallet = PlayerPrefs.GetInt("Wallet");

            int cnt = 0;

            if (Input.GetKeyDown(KeyCode.F)/* && wallet > 15*/ && cnt<=0)
            {
                // ShotGun À¸·Î ¹Ù²Û´Ù
                PlayerPrefs.SetInt("Wallet", wallet - 15);
                gameObject.SetActive(false);
                k = 1;
                cnt++;
            }

        }
    }
}
