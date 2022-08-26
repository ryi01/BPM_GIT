using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_Rifle : MonoBehaviour
{
    private int damage = 2;
    private float range = 100f;

    public Camera fpsCam;
    public Transform muzzlePoint;
    public GameObject muzzleFactory;
    GameObject muzzle;
    public GameObject impactEffect;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    private int maxAmmo = 10;
    public int currentAmmo;
    private int reloadNum = 3;
    private int curNum = 0;
    private bool isReloading = false;

    public Text reload;
    public Text already;

    private float currentTime = 0;

    SR_GunBox gun;
    SR_GunBox1 gun1;

    float dis;
    float dis1;

    AudioSource audio;

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

    public AudioClip[] gunSounds;

    public GameObject animTarget;
    Animator anim;

    public GameObject player;
    //float speed;

    private void Start()
    { 
        currentAmmo = maxAmmo;
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(false);

        // 총기 이미지 활성화 및 비활성화
        pistol.SetActive(false);
        shotGun.SetActive(false);
        rifle.SetActive(true);

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
        if (currentTime > 0.3409f) currentTime -= 0.3409f;
    }

    void Update()
    {
        if (GameManager.Instance.m_state != GameManager.GameState.Playing)
        {
            return;
        }
        // 거리 확인
        dis = Vector3.Distance(transform.position, gun.gameObject.transform.position);
        dis1 = Vector3.Distance(transform.position, gun1.gameObject.transform.position);

        //speed = player.GetComponent<SR_PlayerMove>().finalSpeed;

        if (gameObject.activeSelf == true) anim = animTarget.GetComponent<Animator>();


        if ((currentTime > 0 && currentTime < 0.15f) || (currentTime > 0.1909f && currentTime < 0.3409f))
        {
            if (isReloading) return;

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentAmmo >= maxAmmo)
                {
                    curBullet.text = maxAmmo.ToString();
                    ImageBullet();
                    StartCoroutine(ShowReloaded());
                    audio.clip = gunSounds[2]; audio.Play(); anim.Play("Reload4");
                }
                else
                {
                    curNum++;
                    //print(curNum);
                    if (curNum == 1)
                    {
                        audio.clip = gunSounds[1];
                        audio.Play();
                        anim.Play("Reload1");
                    }
                    if (curNum == 2)
                    {
                        audio.clip = gunSounds[3];
                        audio.Play();
                        anim.Play("Reload2");
                    }
                    if (curNum == 3)
                    {
                        audio.clip = gunSounds[1];
                        audio.Play();
                        anim.Play("Reload3");
                    }
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
                anim.Play("Empty");
            }

            if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire)
            {
                muzzle = Instantiate(muzzleFactory, muzzlePoint);

                nextTimeToFire = Time.time + 1f / fireRate;

                Shoot();
                anim.Play("Shot");
                audio.clip = gunSounds[0];
                audio.Stop();
                audio.Play();

                curNum = 0;
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Blink());
                audio.clip = gunSounds[5];
                audio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.Play("Dash");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("Jump");
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
        reload.gameObject.SetActive(false);
        already.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        already.gameObject.SetActive(false);
    }
    // 이미지 활성화
    void ImageBullet()
    {
        for (int i = 0; i < maxAmmo / 2; i++)
        {
            n = 4;
            bullet[i].gameObject.SetActive(true);
        }
    }
    int n = 4;
    void Shoot()
    {
        currentAmmo -= 2;

        // 총알 이미지 및 텍스트
        curBullet.text = (currentAmmo).ToString();
        bullet[n].gameObject.SetActive(false);
        n--;

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

            if (hit.transform.name.Contains("_Boss")) 
            { 
                BossHP boss = hit.transform.GetComponent<BossHP>();
                if (boss.ENEMYHP > 0)
                {
                    boss.AddDamage(damage, fpsCam.transform.forward);
                }
                else
                {
                    boss.AddDamage(1, fpsCam.transform.forward);
                }
            }
            if (hit.transform.name.Contains("Slow")) Destroy(hit.transform.gameObject); // 보스 Slow Bullet 피격처
            

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
    public IEnumerator RepeatEffect()
    {
        yield return new WaitForSeconds(0.1f);
        muzzle = Instantiate(muzzleFactory,muzzlePoint);
        
    }

    private void OnDisable()
    {

        if (dis < 3)
        {
            gun.count = 2;
        }
        if (dis1 < 3)
        {
            gun1.count = 2;
        }
        
    }
    IEnumerator Blink()
    {
        redCenter.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3409f);
        redCenter.gameObject.SetActive(false);

    }
}
