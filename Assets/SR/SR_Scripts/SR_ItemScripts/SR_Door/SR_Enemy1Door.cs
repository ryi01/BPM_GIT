using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Enemy1Door : MonoBehaviour
{
    int cnt;
    public GameObject cube;
    GameObject[] enemy;
    Animator anim;
    BoxCollider box;

    private void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        cnt = cube.GetComponent<SR_StartToEnemy1>().doorCnt;

        if (cnt == 1 && enemy.Length == 0)
        {
            anim.Play("Open");
            box.enabled = false;
        }
    }
}
