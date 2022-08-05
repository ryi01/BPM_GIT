using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopShotGun : MonoBehaviour
{
    public int k = 0;

    SR_ShopRifle reRifle;
    SR_ShopPistol rePistol;
    SR_WeaponSwitching count;
    SR_WeaponSwitching1 count1;
    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    private void Start()
    {
        reRifle = GameObject.Find("ShopRifle").GetComponent<SR_ShopRifle>();
        rePistol = GameObject.Find("ShopPistol").GetComponent<SR_ShopPistol>();
        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
        count1 = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching1>();
    }
    private void Update()
    {
        if((count.count > 0 || count1.count > 0) && (reRifle || rePistol))
        {
            if(rePistol.k == 1)
            {
                rePistol.k = 0;
            }
            if (reRifle.k == 1)
            {
                reRifle.k = 0;
            }
        }

        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (/* && wallet > 15*/ cnt <= 0)
                {
                    // ShotGun À¸·Î ¹Ù²Û´Ù
                    PlayerPrefs.SetInt("Wallet", wallet - 15);
                    cnt++;
                }
                gameObject.SetActive(false);
                k = 1;
            }
        }
    }

   
}
