using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopPistol : MonoBehaviour
{
    public int k = 0;
    public int k1 = 0;

    SR_ShopRifle reRifle;
    SR_ShopShotGun reShotGun;

    SR_WeaponSwitching count;
    SR_WeaponSwitching1 count1;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    private void Start()
    {
        reRifle = GameObject.Find("ShopRifle").GetComponent<SR_ShopRifle>();
        reShotGun = GameObject.Find("ShopShotGun").GetComponent<SR_ShopShotGun>();

        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
        count1 = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching1>();
    }
    private void Update()
    {
        if (count.count + count1.count > 0)
        {
/*            if (gameObject.transform.parent.name == "Gun Box 1")
            {
                if (reRifle.k == 1)
                {
                    reRifle.k = 0;
                }
                if (reShotGun.k == 1)
                {
                    reShotGun.k = 0;
                }
            }*/
            if (gameObject.transform.parent.name == "Gun Box 2")
            {
                if (reRifle.k1 == 1)
                {
                    reRifle.k1 = 0;
                }
                if (reShotGun.k1 == 1)
                {
                    reShotGun.k1 = 0;
                }
            }
        }

        player = GameObject.Find("Player").transform;

        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - transform.position;
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
                
                if(gameObject.transform.parent.name == "Gun Box 1")
                {
                    k++;
                }           
                if(gameObject.transform.parent.name == "Gun Box 2")
                {
                    k1++;
                }
                
            }
        }
    }
}
