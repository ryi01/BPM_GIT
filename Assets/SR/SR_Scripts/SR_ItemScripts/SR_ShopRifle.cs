using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopRifle : MonoBehaviour
{
    public int k = 0;
    SR_ShopShotGun reShotGun;
    SR_WeaponSwitching count;
    private void Start()
    {
        reShotGun = GameObject.Find("ShopShotGun").GetComponent<SR_ShopShotGun>();
        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
    }
    private void Update()
    {
        if (reShotGun && count.count > 0)
        {
            if (reShotGun.k == 1) reShotGun.k = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            int wallet = PlayerPrefs.GetInt("Wallet");

            int cnt = 0;

            if (Input.GetKeyDown(KeyCode.F) /*&& wallet > 5 */&& cnt<=0)
            {
                // Rifle �� �ٲ۴�
                PlayerPrefs.SetInt("Wallet", wallet - 5);
                gameObject.SetActive(false);
                k = 1;
                cnt++;
            }

        }
    }
}
