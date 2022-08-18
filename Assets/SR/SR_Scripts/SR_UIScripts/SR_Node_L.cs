using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Node_L : MonoBehaviour
{
    public int dir;
    public bool isCheck;
    Transform left;

    private void Start()
    {
        left = GameObject.Find("LeftPop").transform;
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
        transform.position += -(gameObject.transform.position - left.position) * dir * SR_BPM.instance.nodeSpeed * Time.deltaTime;

        if((gameObject.transform.position - left.position).magnitude < 0.035f)
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
