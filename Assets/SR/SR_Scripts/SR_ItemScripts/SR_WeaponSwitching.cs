using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SR_WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 1;

    // Ω∫≈‰æÓ √—µÈ
    public GameObject gunPistol;
    public GameObject gunRifle;
    public GameObject gunShotGun;

    public float dis;

    // √— √£±‚
    SR_ShopRifle rifle;
    SR_ShopShotGun shotgun;
    SR_ShopPistol pistol;

    public int count = 0;

    void Start()
    {
        SelectedWeapon(0);
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "3 StpreScene")
        {
            // √—±‚ »Æ¿Œ
            rifle = gunRifle.GetComponent<SR_ShopRifle>();
            shotgun = gunShotGun.GetComponent<SR_ShopShotGun>();
            pistol = gunPistol.GetComponent<SR_ShopPistol>();

            if (pistol)
            {
                if (pistol.k > 0)
                {
                    selectedWeapon = 0;
                    SelectedWeapon(0);
                    count = 1;
                }
            }
            if (shotgun)
            {
                if (shotgun.k > 0)
                {
                    selectedWeapon = 1;
                    SelectedWeapon(1);
                    count = 1;
                }
            }
            if (rifle)
            {
                if (rifle.k > 0)
                {
                    selectedWeapon = 2;
                    SelectedWeapon(2);
                    count = 1;
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
