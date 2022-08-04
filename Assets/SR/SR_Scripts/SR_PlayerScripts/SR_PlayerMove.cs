using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_PlayerMove : MonoBehaviour
{
    public float speed = 7.0f;
    float finalSpeed;
    float gravity = -9.8f;
    public float jumpPower = 3;
    float yVelocity;
    int jumpCnt = 0;

    public float dashSpeed;

    CharacterController cc;

    public bool dashing;

    Rigidbody rigid;

    Vector3 dir;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        finalSpeed = speed;
        
        //¸ðµç ¿òÁ÷ÀÓ
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = transform.right * h + transform.forward * v;
        dir.Normalize();
        dir.y = yVelocity;

        //Â«Çª
        yVelocity += gravity * Time.deltaTime;
        
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCnt < 1)
            {
                jumpCnt++;
                yVelocity = jumpPower;
            }
        }
        if (cc.isGrounded == true) jumpCnt = 0;

        //´ë½¬
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
        yield return new WaitForSeconds(1.0f);
        yVelocity = -9.8f;
    }

    public void NockBack(float amount)
    {
        rigid.AddForce(-transform.forward * amount,ForceMode.Impulse);
    }
}
