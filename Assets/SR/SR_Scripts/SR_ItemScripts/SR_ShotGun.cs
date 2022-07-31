using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_ShotGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public Transform muzzlePoint;
    public GameObject muzzleFactory;
    GameObject muzzle;
    public GameObject impactEffect;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading) return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            muzzle = Instantiate(muzzleFactory,muzzlePoint);

            nextTimeToFire = Time.time + 1f / fireRate;


            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading....");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
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
