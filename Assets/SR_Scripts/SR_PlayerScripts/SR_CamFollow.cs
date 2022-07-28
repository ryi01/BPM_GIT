using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_CamFollow : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = target.position;
    }
}
