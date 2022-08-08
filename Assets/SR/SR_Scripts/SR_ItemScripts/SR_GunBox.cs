using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스토어 총기 박스 : 
public class SR_GunBox : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (guns.count <= 0)
        {
            SelectedWeapon(1);
        }
        if (guns.count > 0)
        {
            SelectedWeapon(count);
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
