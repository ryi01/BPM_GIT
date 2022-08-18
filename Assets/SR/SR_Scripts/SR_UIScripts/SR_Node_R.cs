using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Node_R : MonoBehaviour
{
    public int dir;
    public bool isCheck;

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
        transform.position += transform.right * dir * SR_BPM.instance.nodeSpeed * Time.deltaTime;
        if ((gameObject.transform.position - new Vector3(960, 540, 0)).magnitude < 70)
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
