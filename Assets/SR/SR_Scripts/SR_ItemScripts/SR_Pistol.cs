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

    SR_GunBox gun;
    SR_GunBox gun1;

    float dis;
    float dis1;


    private void Start()
    {
        currentAmmo = maxAmmo;
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(false);

        gun = GameObject.Find("Gun Box 1").GetComponent<SR_GunBox>();
        gun1 = GameObject.Find("Gun Box 2").GetComponent<SR_GunBox>();
    }
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.3375f) currentTime = 0;
    }

    void Update()
    {

        // �Ÿ� Ȯ��
        dis = Vector3.Distance(transform.position, gun.transform.position);
        dis1 = Vector3.Distance(transform.position, gun1.transform.position);

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

        int layer = 1 << 7;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~layer))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.name.Contains("Larva"))
            {
                hit.transform.GetComponent<LarvaHP>().AddDamage(damage);
                hit.transform.GetComponent<Larva>().NockBack();
            }

            if (hit.transform.name.Contains("Bat")) hit.transform.GetComponent<BatHP>().AddDamage(damage);

            if (hit.transform.name.Contains("Spider"))
            {
                hit.transform.GetComponent<SpiderHP>().AddDamage(damage);
                hit.transform.GetComponent<Spider>().NockBack();

            }


            if (hit.transform.name.Contains("Boss")) hit.transform.GetComponent<BossHP>().AddDamage(damage);

            if (hit.transform.name.Contains("Slow")) Destroy(hit.transform); // ���� Slow Bullet �ǰ�ó


            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }
    }
    private void OnDisable()
    {
        if (dis < 3)
        {
            gun.count = 0;
        }
        if (dis1 < 3)
        {
            gun1.count = 0;
        }
    }
}
