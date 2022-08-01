using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_ShotGun : MonoBehaviour
{
    private float damage = 4f;
    private float range = 100f;

    public Camera fpsCam;
    public Transform muzzlePoint;
    public GameObject muzzleFactory;
    GameObject muzzle;
    public GameObject impactEffect;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    private int maxAmmo = 1;
    private int currentAmmo;
    private int reloadNum = 3;
    private int curNum = 0;
    private float reloadTime = 1f;
    private bool isReloading = false;

    public Text reload;
    public Text already;

    private void Start()
    {
        currentAmmo = maxAmmo;
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isReloading) return;



        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo >= maxAmmo)
            {
                StartCoroutine(ShowReloaded());
            }
            else
            {
                curNum++;
                print(curNum);
            }
        }




        if (curNum >= reloadNum)
        {
            StartCoroutine(Reload());
            curNum = 0;
            return;
        }

        if (Input.GetButtonDown("Fire1") && currentAmmo <= 0)
        {

            StartCoroutine(ShowReload());
        }

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire)
        {
            muzzle = Instantiate(muzzleFactory, muzzlePoint);

            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();
            curNum = 0;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloaded!");
        yield return new WaitForSeconds(0f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    IEnumerator ShowReload()
    {
        reload.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        reload.gameObject.SetActive(false);
    }
    IEnumerator ShowReloaded()
    {
        already.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        already.gameObject.SetActive(false);
    }
    void Shoot()
    {
        currentAmmo--;

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            //타겟 데미지 입히는 부분
            /*
            Target target = hit.transform.Getcomponent<Target>();
            if(target!=null)
            {
                target.TakeDamage(damage);
            }
            */
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }
    }
    
}
