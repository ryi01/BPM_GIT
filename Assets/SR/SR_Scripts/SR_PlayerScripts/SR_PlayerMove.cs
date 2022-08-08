using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_PlayerMove : MonoBehaviour
{
    public float speed = 15.0f;
    float finalSpeed;
    float gravity = -9.8f;
    public float jumpPower = 4;
    public float yVelocity;
    int jumpCnt = 0;

    public float dashSpeed = 100;

    CharacterController cc;

    public bool dashing;

    Rigidbody rigid;

    Vector3 dir;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //짬푸
        yVelocity += gravity * Time.deltaTime;
        
        if (dashing == false && Input.GetButtonDown("Jump"))
        {
            if (jumpCnt < 1)
            {
                jumpCnt++;
                yVelocity = jumpPower;
            }
        }
        if (cc.isGrounded == true) jumpCnt = 0;


        //치트###
        GameObject boss = GameObject.Find("Boss");
        GameObject enemy = GameObject.Find("EnemyManager");
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (boss)
            {

                 Destroy(boss.gameObject);
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
