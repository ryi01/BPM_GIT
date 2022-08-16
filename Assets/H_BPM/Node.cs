using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int dir;
    public bool isCheck;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(isCheck)
            {
                Bpm.instance.Shot();
                Destroy(gameObject);
            }
        }

        transform.position += transform.right * dir * Bpm.instance.nodeSpeed * Time.deltaTime;

        //if(dir == -1 && transform.position.x <=0 )
        //{
        //    Bpm.instance.Shot();
        //    Destroy(gameObject);
        //}
        //else if(dir == 1 && transform.position.x >= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        isCheck = true;
        print("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        isCheck = false;
        print("Exit");
        Destroy(gameObject);
    }
}
