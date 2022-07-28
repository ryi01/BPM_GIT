using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_GunSystem : MonoBehaviour
{

    //Gun Stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShoots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int buletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;


    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetButton("Fire1");
        else shooting = Input.GetButtonDown("Fire1");

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit,range,whatisEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy")) rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
        }
        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        Reload = false;
    }
}
