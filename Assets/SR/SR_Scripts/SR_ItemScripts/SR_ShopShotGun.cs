using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShopShotGun : MonoBehaviour
{
    public int k = 0;
    SR_ShopRifle reRifle;
    SR_WeaponSwitching count;

    Transform player;
    Vector3 dis;

    int cnt = 0;

    public float senseDis = 3;

    private void Start()
    {
        reRifle = GameObject.Find("ShopRifle").GetComponent<SR_ShopRifle>();
        count = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
    }
    private void Update()
    {
        if (reRifle && count.count > 0)
        {
            if (reRifle.k == 1) reRifle.k = 0;
        }

        player = GameObject.Find("Player").transform;
        int wallet = PlayerPrefs.GetInt("Wallet");

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (Input.GetKeyDown(KeyCode.F)/* && wallet > 15*/ && cnt <= 0)
            {
                // ShotGun ���� �ٲ۴�
                PlayerPrefs.SetInt("Wallet", wallet - 15);
                gameObject.SetActive(false);
                k = 1;
                cnt++;
            }
        }
    }

   
}
