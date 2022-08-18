using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Node_R : MonoBehaviour
{
    public int dir;
    public bool isCheck;
    Transform right;

    private void Start()
    {
        right = GameObject.Find("RightPop").transform;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(isCheck)
            {
                //SR_BPM.instance.Shot();
                Destroy(gameObject);
            }
        }
        transform.position += (gameObject.transform.position - right.position) * dir * SR_BPM.instance.nodeSpeed * Time.deltaTime; if ((gameObject.transform.position - right.position).magnitude <= 0.035f)
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    isCheck = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    isCheck = false;
    //    Destroy(gameObject);
    //}

}
