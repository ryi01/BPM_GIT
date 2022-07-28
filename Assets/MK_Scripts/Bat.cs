using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Dummy_Player");
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();

        transform.position += dir * 2 * Time.deltaTime;
    }
}
