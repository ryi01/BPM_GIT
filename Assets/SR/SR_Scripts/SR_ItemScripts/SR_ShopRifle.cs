using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopRifle : MonoBehaviour
{
    public int k = 0;
    SR_ShopShotGun reShotGun;
    SR_WeaponSwitching count;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

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

        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {

            if (Input.GetKeyDown(KeyCode.F) /*&& wallet > 5 */&& cnt <= 0)
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
