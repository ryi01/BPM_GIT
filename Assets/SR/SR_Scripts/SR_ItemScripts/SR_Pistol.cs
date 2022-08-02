using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_Pistol : MonoBehaviour
{
    private int damage = 1;
    private float range = 100f;

    public Camera fpsCam;
    public Transform muzzlePoint;
    public GameObject muzzleFactory;
    GameObject muzzle;
    public GameObject impactEffect;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    private int maxAmmo = 6;
    private int currentAmmo;
    private int reloadNum=2;
    private int curNum=0;
    private bool isReloading = false;

    public Text reload;
    public Text already;

    private float currentTime = 0;

    private void Start()
    {
        currentAmmo = maxAmmo;
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.3375f) currentTime = 0;
    }

    void Update()
    {
        if ((currentTime > 0 && currentTime < 0.15f) || (currentTime > 0.1875f && currentTime < 0.3375f))
        {

            if (isReloading) return;



            if (Input.GetKeyDown(KeyCode.R))
            {
                if(currentAmmo >=maxAmmo)
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
                muzzle = Instantiate(muzzleFactory,muzzlePoint);

                nextTimeToFire = Time.time + 1f / fireRate;
            
                Shoot();
                curNum = 0;
            }
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

            GameObject larva = GameObject.Find("Larva");
            if (hit.transform.name.Contains("Larva"))
            {
                larva.GetComponent<LarvaHP>().AddDamage(damage);
                larva.GetComponent<Larva>().NockBack();
            }

            GameObject bat = GameObject.Find("Bat");
            if (hit.transform.name.Contains("Bat")) bat.GetComponent<BatHP>().AddDamage(damage);

            GameObject spider = GameObject.Find("Spider");
            if (hit.transform.name.Contains("Spider"))
            {
                spider.GetComponent<SpiderHP>().AddDamage(damage);
                spider.GetComponent<Spider>().NockBack();

            }

            GameObject boss = GameObject.Find("Boss");
            if (hit.transform.name.Contains("Boss")) boss.GetComponent<BossHP>().AddDamage(damage);

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }
    }
}
