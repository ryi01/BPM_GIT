using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 1;

    public GameObject gunRifle;
    public GameObject gubShotGun;

    void Start()
    {
        SelectedWeapon(0);
    }

    
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        int a = selectedWeapon;

        int rifle = gunRifle.GetComponent<SR_ShopRifle>().k;
        int shotgun = gubShotGun.GetComponent<SR_ShopShotGun>().k;

        if (selectedWeapon > 4) selectedWeapon = a % 4;
        // when weapon system completed, change this area.
        // change by wheel -> change by selected gun item.
        //if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        //{
        //    if (selectedWeapon >= transform.childCount - 1) selectedWeapon = 0;
        //    else selectedWeapon++;


        //}
        //if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        //{
        //    if (selectedWeapon <= 0) selectedWeapon = transform.childCount - 1;
        //    else selectedWeapon++;
        //}
        //if(previousSelectedWeapon != selectedWeapon)
        //{
        //    SelectedWeapon();
        //}
        if(rifle > 0)
        {
            selectedWeapon = 2;
            SelectedWeapon(2);
        }
        if (shotgun > 0)
        {
            selectedWeapon = 1;
            SelectedWeapon(1);

        }
        
    }
    void SelectedWeapon(int selectedWeapon)
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
            
        }
    }
}
