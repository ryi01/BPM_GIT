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
    public int currentAmmo;
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
    new AudioSource audio;

    // 총알 개수
    public Text curBullet;
    public Text totalBullet;
    // 총알 이미지
    public Image[] bullet;

    [SerializeField]
    // 총기 UI
    public GameObject pistol;
    public GameObject shotGun;
    public GameObject rifle;

    public Image redCenter;
    // 총 쏘는 중 넘기기
    public bool isAttack = false;

    public AudioClip[] gunSounds;


    private void Start()
    {
        currentAmmo = maxAmmo;
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(false);

        // 총기 이미지 활성화 및 비활성화
        pistol.SetActive(true);
        shotGun.SetActive(false);
        rifle.SetActive(false);

        // 최대 총알 개수
        totalBullet.text = maxAmmo.ToString();
        curBullet.text = currentAmmo.ToString();


        gun = GameObject.Find("Gun Box 1").GetComponent<SR_GunBox>();
        gun1 = GameObject.Find("Gun Box 2").GetComponent<SR_GunBox1>();
        

        audio = GetComponent<AudioSource>();

    }
    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime > 0.3409f ) currentTime -= 0.3409f;
    }

    void Update()
    {
        // 거리 확인
        /*        dis = Vector3.Distance(transform.position, gun.gameObject.transform.position);
                dis1 = Vector3.Distance(transform.position, gun1.gameObject.transform.position);*/


        if ((currentTime >0 && currentTime < 0.15f) || (currentTime > 0.1909f && currentTime < 0.3409f))
        {

            if (isReloading) return;



            if (Input.GetKeyDown(KeyCode.R))
            {
                if(currentAmmo >=maxAmmo)
                {
                    curBullet.text = maxAmmo.ToString();
                    ImageBullet();
                    StartCoroutine(ShowReloaded());
                    audio.clip = gunSounds[2]; audio.Play();
                }
                else
                {
                    curNum++;
                    //print(curNum);
                    //재장전 소리
                    if (curNum == 1) audio.clip = gunSounds[1]; audio.Play();
                    if (curNum == 2) audio.clip = gunSounds[3]; audio.Play();
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
                audio.clip = gunSounds[4];
                audio.Play();
                StartCoroutine(ShowReload());
            }

            if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire)
            {
                muzzle = Instantiate(muzzleFactory,muzzlePoint);

                nextTimeToFire = Time.time + 1f / fireRate;
            
                Shoot();
                audio.clip = gunSounds[0];
                audio.Stop();
                audio.Play();
                curNum = 0;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.R))
            {
                isAttack = true;
                StartCoroutine(Blink());
                audio.clip = gunSounds[5];
                audio.Play();
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
    // 이미지 활성화
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

        // 총알 이미지 및 텍스트
        curBullet.text = (currentAmmo).ToString();
        bullet[currentAmmo].gameObject.SetActive(false);

        RaycastHit hit;

        int layer = 1 << 7;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~layer))
        {
            //Debug.Log(hit.transform.name);

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


            if (hit.transform.name == "_Boss") hit.transform.GetComponent<BossHP>().AddDamage(damage, fpsCam.transform.forward);

            if (hit.transform.name.Contains("Slow")) Destroy(hit.transform.gameObject); // 보스 Slow Bullet 피격처
            


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
    IEnumerator Blink()
    {
        redCenter.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3409f);
        redCenter.gameObject.SetActive(false);

    }
}
