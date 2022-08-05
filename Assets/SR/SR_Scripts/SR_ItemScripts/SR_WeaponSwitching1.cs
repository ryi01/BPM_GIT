using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_WeaponSwitching1 : MonoBehaviour
{
    public int selectedWeapon = 1;

    // ����� �ѵ�
    public GameObject gunPistol;
    public GameObject gunRifle;
    public GameObject gunShotGun;

    public float dis;

    // ���� ���� ����
    GameObject preWeapon;

    // �� ã��
    SR_ShopRifle rifle;
    SR_ShopShotGun shotgun;
    SR_ShopPistol pistol;



    public int count = 0;
    int a;

    void Start()
    {
        SelectedWeapon(0);

        preWeapon = GameObject.Find("Gun Box 2");
    }


    void Update()
    {
        // �ѱ� Ȯ��
        rifle = gunRifle.GetComponent<SR_ShopRifle>();
        shotgun = gunShotGun.GetComponent<SR_ShopShotGun>();
        pistol = gunPistol.GetComponent<SR_ShopPistol>();

        dis = Vector3.Distance(transform.position, preWeapon.transform.position);
        if (dis < 3)
        {
            if (pistol)
            {
                if (pistol.k > 0)
                {
                    selectedWeapon = 0;
                    SelectedWeapon(0);
                    count++;
                }
            }
            if (shotgun)
            {
                if (shotgun.k > 0)
                {
                    selectedWeapon = 1;
                    SelectedWeapon(1);
                    count++;
                }
            }
            if (rifle)
            {
                if (rifle.k > 0)
                {
                    selectedWeapon = 2;
                    SelectedWeapon(2);
                    count++;
                }
            }
        }
    }
    void SelectedWeapon(int selectedWeapon)
    {
        
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon) 
            { 
                weapon.gameObject.SetActive(true);

            }
            else weapon.gameObject.SetActive(false);
            i++;
            
        }
        
    }
}
