using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� �ѱ� �ڽ� : 
public class SR_GunBox : MonoBehaviour
{
    // SR_WeaponSwitching���� selectedWeapon ���� �����ͼ� �ѱ�
    SR_WeaponSwitching guns;

    // �ȿ� �ִ� �� ��ũ��Ʈ ������Ʈ ��������
    SR_ShopPistol rePistol;
    SR_ShopShotGun reShotGun;
    SR_ShopRifle reRifle;

    // ��� �ִ� ��
    public int preGuns = 0;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        // SR_WeaponSwitching ã��
        guns = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching>();

        rePistol = GetComponentInChildren<SR_ShopPistol>();
        reShotGun = GetComponentInChildren<SR_ShopShotGun>();
        reRifle = GetComponentInChildren<SR_ShopRifle>();
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (reRifle.k == 1) reRifle.k = 0;
                if (rePistol.k == 1) rePistol.k = 0;
                if (reShotGun.k == 1) reShotGun.k = 0;
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
