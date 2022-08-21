using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Scroll : MonoBehaviour
{
    Transform player;
    Vector3 dis;

    public float senseDis = 5;

    int nScroll;

    private void Update()
    {
        player = GameObject.Find("Player").transform;
        nScroll = player.GetComponent<SR_PlayerInventory>().numberOfScrolls;

        //int skill = PlayerPrefs.GetInt("Skill");

        SR_PlayerInventory playerInventory = player.GetComponent<SR_PlayerInventory>();

        dis = player.position - gameObject.transform.position;
        if (dis.magnitude <= senseDis)
        {
            if (playerInventory != null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    print("F");
                    playerInventory.ScrollCollected();
                    //Destroy(gameObject);
                    //PlayerPrefs.SetInt("Skill", 1);
                    nScroll = 1;
                    player.GetComponent<SR_PlayerInventory>().numberOfScrolls = nScroll;
                    Destroy(gameObject);
                }
            }
        }
    }
}