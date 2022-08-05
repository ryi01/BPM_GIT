using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopRifle : MonoBehaviour
{
    public int k = 0;
    SR_ShopShotGun reShotGun;
    SR_ShopPistol rePistol;
    SR_WeaponSwitching count;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    private void Start()
    {
        reShotGun = GameObject.Find("ShopShotGun").GetComponent<SR_ShopShotGun>();
        rePistol = GameObject.Find("ShopShotGun").GetComponent<SR_ShopPistol>();
        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
    }
    private void Update()
    {
        if (count.count > 0 && (reShotGun || rePistol))
        {
            if (rePistol.k == 1)
            {
                rePistol.k = 0;
            }
            if (reShotGun.k == 1)
            {
                reShotGun.k = 0;
            }
        }

        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (/* && wallet > 5*/ cnt <= 0)
                {
                    // ShotGun À¸·Î ¹Ù²Û´Ù
                    PlayerPrefs.SetInt("Wallet", wallet - 5);
                }
                gameObject.SetActive(false);
                k = 1;
                cnt++;
            }

        }
    }
 
}
