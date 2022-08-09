using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스토어 총기 박스 : 
public class SR_GunBox1 : MonoBehaviour
{
    // SR_WeaponSwitching에서 selectedWeapon 변수 가져와서 켜기
    SR_WeaponSwitching1 guns;

    // 팔에 있는 총 스크립트 컴포넌트 가져오기
    SR_ShopPistol rePistol;
    SR_ShopShotGun reShotGun;
    SR_ShopRifle reRifle;

    // 들고 있는 총
    public int preGuns = 0;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        // SR_WeaponSwitching 찾기
        guns = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching1>();

        rePistol = GetComponentInChildren<SR_ShopPistol>();
        reShotGun = GetComponentInChildren<SR_ShopShotGun>();
        reRifle = GetComponentInChildren<SR_ShopRifle>();
    }

    // Update is called once per frame
    void Update()
    {

        if(guns.count <= 0)
        {
            SelectedWeapon(2);
        }
        if (guns.count > 0)
        {
            SelectedWeapon(count);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (reRifle.k1 == 1) reRifle.k1 = 0;
                if (rePistol.k1 == 1) rePistol.k1 = 0;
                if (reShotGun.k1 == 1) reShotGun.k1 = 0;
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
