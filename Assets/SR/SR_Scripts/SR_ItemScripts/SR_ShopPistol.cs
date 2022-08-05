using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopPistol : MonoBehaviour
{
    public int k = 0;
    SR_ShopRifle reRifle;
    SR_ShopShotGun reShotGun;
    SR_GunBox preWeapon;
    SR_WeaponSwitching count;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    private void Start()
    {
        reRifle = GameObject.Find("ShopRifle").GetComponent<SR_ShopRifle>();
        reShotGun = GameObject.Find("ShopShotGun").GetComponent<SR_ShopShotGun>();
        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
        preWeapon = GameObject.Find("Gun Box 1").GetComponent<SR_GunBox>();
    }
    private void Update()
    {
        if (count.count > 0 && (reShotGun || reRifle))
        {
            if (reShotGun.k == 1)
            {
                reShotGun.k = 0;
            }
            if (reRifle.k == 1)
            {
                reRifle.k = 0;
            }
        }

        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (/* && wallet > 15 && */ cnt <= 0)
                {
                    // ShotGun À¸·Î ¹Ù²Û´Ù
                    PlayerPrefs.SetInt("Wallet", wallet - 15);
                    cnt++;
                }
                gameObject.SetActive(false);
                preWeapon.count = 1;
                k = 1;
            }
        }
    }

}
