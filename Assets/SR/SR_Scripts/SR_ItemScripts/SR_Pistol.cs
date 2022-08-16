using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    SR_GunBox1 gun1;

    float dis;
    float dis1;

    AudioSource audio;

    // �Ѿ� ����
    public Text curBullet;
    public Text totalBullet;
    // �Ѿ� �̹���
    public Image[] bullet;

    [SerializeField]
    // �ѱ� UI
    public GameObject pistol;
    public GameObject shotGun;
    public GameObject rifle;


    private void Start()
    {
        currentAmmo = maxAmmo;
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(false);

        // �ѱ� �̹��� Ȱ��ȭ �� ��Ȱ��ȭ
        pistol.SetActive(true);
        shotGun.SetActive(false);
        rifle.SetActive(false);

        // �ִ� �Ѿ� ����
        totalBullet.text = maxAmmo.ToString();
        curBullet.text = currentAmmo.ToString();

        if (SceneManager.GetActiveScene().name == "3 StoreScene")
        {
            gun = GameObject.Find("Gun Box 1").GetComponent<SR_GunBox>();
            gun1 = GameObject.Find("Gun Box 2").GetComponent<SR_GunBox1>();
        }

        audio = GetComponent<AudioSource>();

    }
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.3409f) currentTime -= 0.3409f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "3 StoreScene")
        {
            // �Ÿ� Ȯ��
            dis = Vector3.Distance(transform.position, gun.gameObject.transform.position);
            dis1 = Vector3.Distance(transform.position, gun1.gameObject.transform.position);
        }

        if ((currentTime > 0 && currentTime < 0.15f) || (currentTime > 0.1909f && currentTime < 0.3409f))
        {

            if (isReloading) return;



            if (Input.GetKeyDown(KeyCode.R))
            {
                if(currentAmmo >=maxAmmo)
                {
                    curBullet.text = maxAmmo.ToString();
                    ImageBullet();
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
                audio.Play();
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
    // �̹��� Ȱ��ȭ
    void ImageBullet()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            bullet[i].gameObject.SetActive(true);
        }
    }
    void Shoot()
    {
        currentAmmo--;

        // �Ѿ� �̹��� �� �ؽ�Ʈ
        curBullet.text = (currentAmmo).ToString();
        bullet[currentAmmo].gameObject.SetActive(false);

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

            if (SceneManager.GetActiveScene().name == "7 BossScene")
            {
                if (hit.transform.name == "Boss") hit.transform.GetComponent<BossHP>().AddDamage(damage);

                if (hit.transform.name.Contains("Slow")) Destroy(hit.transform.gameObject); // ���� Slow Bullet �ǰ�ó
            }


            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }
    }
    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "3 StoreScene")
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
}
