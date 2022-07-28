using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;
    float finalSpeed;
    float gravity = -9.8f;
    public float jumpPower = 3;
    float yVelocity;
    int jumpCnt = 0;

    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        finalSpeed = speed;
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

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = transform.right * h + transform.forward * v;
        dir.Normalize();
        dir.y = yVelocity;

        

        cc.Move(dir * finalSpeed * Time.deltaTime);
    }

}
