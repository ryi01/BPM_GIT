using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopRifle : MonoBehaviour
{
    public int k = 0;
    public int k1 = 0;


    SR_ShopShotGun reShotGun;
    SR_ShopPistol rePistol;
    SR_WeaponSwitching count;
    SR_WeaponSwitching1 count1;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    GameObject box;
    GameObject box1;

    private void Start()
    {
        reShotGun = GameObject.Find("ShopShotGun").GetComponent<SR_ShopShotGun>();
        rePistol = GameObject.Find("ShopPistol").GetComponent<SR_ShopPistol>();

        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
        count1 = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching1>();

        box = GameObject.Find("Gun Box 1");
        box1 = GameObject.Find("Gun Box 2");
    }
    private void Update()
    {
        if (count.count + count1.count > 0)
        {
/*            if (gameObject.transform.parent.name == "Gun Box 1")
            {
                if (rePistol.k == 1)
                {
                    rePistol.k = 0;
                }
                if (reShotGun.k == 1)
                {
                    reShotGun.k = 0;
                }
            }*/
            if (gameObject.transform.parent.name == "Gun Box 2")
            {
                if (rePistol.k1 == 1)
                {
                    rePistol.k1 = 0;
                }
                if (reShotGun.k1 == 1)
                {
                    reShotGun.k1 = 0;
                }
            }
        }

        player = GameObject.Find("Player").transform;

        dis = player.position - transform.position;

        int wallet = PlayerPrefs.GetInt("Wallet");
        if (dis.magnitude <= senseDis)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (/* && wallet > 5*/ cnt <= 0)
                {
                    // ShotGun À¸·Î ¹Ù²Û´Ù
                    PlayerPrefs.SetInt("Wallet", wallet - 5);
                    cnt++;
                }
                gameObject.SetActive(false);

                if (gameObject.transform.parent.name == "Gun Box 1")
                {
                    k++;
                }
                if (gameObject.transform.parent.name == "Gun Box 2")
                {
                    k1++;
                }
            }

        }
    }

}
