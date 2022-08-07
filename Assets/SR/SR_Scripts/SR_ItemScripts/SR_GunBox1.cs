using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� �ѱ� �ڽ� : 
public class SR_GunBox1 : MonoBehaviour
{
    // SR_WeaponSwitching���� selectedWeapon ���� �����ͼ� �ѱ�
    SR_WeaponSwitching1 guns;

    // ��� �ִ� ��
    public int preGuns = 0;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        // SR_WeaponSwitching ã��
        guns = GameObject.Find("Guns").GetComponent<SR_WeaponSwitching1>();
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
