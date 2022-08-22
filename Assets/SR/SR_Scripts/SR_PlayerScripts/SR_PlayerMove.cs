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

    void Start()
    {
        cc = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        ui = GameObject.Find("Main Camera").GetComponentInChildren<UIShake>();
    }
    private void Update()
    {
        bool ground = cc.isGrounded;
        //짬푸
        yVelocity += gravity * Time.deltaTime;
        if (dashing == false && Input.GetButtonDown("Jump"))
        {
            jumpCnt++;
            ui.Shaking();
            if (jumpCnt < 2)
            {
                yVelocity = jumpPower;
            }
            ground = false;
        }
       
        if (ground == true && jumpCnt > 0)
        {
            jumpCnt = 0;
            shake = 0;
            ui.Shaking();

        }


        //치트###적 없애기
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
        
        //모든 움직임
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = transform.right * h + transform.forward * v;
        dir.Normalize();
        dir.y = yVelocity;

        //대쉬
        if(dashing)
        {
            finalSpeed = dashSpeed;
            StartCoroutine(FalseGravity());
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
