using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 300f;

    public float x = 0;

    public float mx = 0;
    public float y = 0;

    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(x, mx, y);
    }
}
