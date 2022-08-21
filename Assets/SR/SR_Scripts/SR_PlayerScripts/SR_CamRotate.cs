using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_CamRotate : MonoBehaviour
{
    public float rotSpeed = 300.0f;

    public float mx = 0;
    public float my = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        my = -transform.eulerAngles.x;
        mx = transform.eulerAngles.y;
    }
    void LateUpdate()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -90f, 90f);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
