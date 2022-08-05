using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스토어 총기 박스 : 
public class SR_GunBox : MonoBehaviour
{
    // 들고 있는 총
    SR_ShopPistol pistol;
    SR_ShopShotGun shotGun;
    SR_ShopRifle rifle;
    // SR_WeaponSwitching에서 selectedWeapon 변수 가져와서 켜기
    SR_WeaponSwitching guns;
    SR_WeaponSwitching1 guns1;
    // 들고 있는 총
    public int preGuns = 0;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        // SR_WeaponSwitching 찾기
        guns = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();
        guns1 = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Gun Box 1")
        {
            SelectedWeapon(1);
        }
        if (gameObject.name == "Gun Box 2")
        {
            SelectedWeapon(2);
        }
        if (guns.dis < 3)
        {
            if (guns.count > 0)
            {
                SelectedWeapon(count);
            }
        }

        if (guns1.dis < 3)
        {
            if (guns1.count > 0)
            {
                print(count);
                SelectedWeapon(count);
            }
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
