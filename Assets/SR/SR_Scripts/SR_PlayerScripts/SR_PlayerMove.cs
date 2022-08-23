using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SR_PlayerMove : MonoBehaviour
{
    public float speed = 15.0f;
    public float finalSpeed;
    float gravity = -9.8f;
    public float jumpPower = 4;
    public float yVelocity;
    int jumpCnt = 0;
    int shake = 0;
    bool jump = false;

    public float dashSpeed = 100;

    CharacterController cc;

    public bool dashing;

    Rigidbody rigid;

    Vector3 dir;

    UIShake ui;

    AudioSource audio;

    

    void Start()
    {
        cc = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        ui = GameObject.Find("Main Camera").GetComponentInChildren<UIShake>();
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        bool ground = cc.isGrounded;
        //«Ǫ
        yVelocity += gravity * Time.deltaTime;
        if (dashing == false && Input.GetButtonDown("Jump"))
        {
            ground = false;
            jumpCnt++;
            audio.clip = GetComponent<SR_PlayerSound>().playerSounds[0];
            audio.Play();
            ui.Shaking();
            if (jumpCnt < 2)
            {
                yVelocity = jumpPower;
            }

        }
       
        if (ground == true && jumpCnt > 0)
        {
            jumpCnt = 0;
            shake = 0;
            ui.Shaking();

        }


        //ġƮ###�� ���ֱ�
        GameObject boss = GameObject.Find("_Boss");
        GameObject enemy = GameObject.Find("EnemyManager");
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (boss)
            {
                boss.GetComponent<BossHP>().ENEMYHP = 0;
            }
            if(enemy) Destroy(enemy.gameObject);
        }

    }
    void FixedUpdate()
    {
        finalSpeed = speed;
        
        //��� ������
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = transform.right * h + transform.forward * v;
        dir.Normalize();
        dir.y = yVelocity;

        //�뽬
        if (dashing)
        {
            audio.clip = GetComponent<SR_PlayerSound>().playerSounds[1];
            audio.Play();
            finalSpeed = dashSpeed;
            StartCoroutine(FalseGravity());
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            audio.clip = GetComponent<SR_PlayerSound>().playerSounds[2];
            audio.Play();
        }

        cc.Move(dir * finalSpeed * Time.deltaTime);
    }
    IEnumerator FalseGravity()
    {
        yVelocity = 0;
        yield return new WaitForSeconds(2.0f);
    }

    public void NockBack(float amount)
    {
        rigid.AddForce(-transform.forward * amount,ForceMode.Impulse);
    }
}
