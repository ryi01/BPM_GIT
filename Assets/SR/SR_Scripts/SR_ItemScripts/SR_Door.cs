using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Door : MonoBehaviour
{
    Transform player;
    Vector3 dis;

    GameObject door;

    public float senseDis = 3;

    bool opening = false;

    int nKey;

    private void Update()
    {
        door = GameObject.Find("Door");
        player = GameObject.Find("Player").transform;
        //int pouch = PlayerPrefs.GetInt("Pouch");
        nKey = player.GetComponent<SR_PlayerInventory>().numberOfKeys;

        SR_PlayerInventory playerInventory = player.GetComponent<SR_PlayerInventory>();

        dis = player.position - gameObject.transform.position;


        if (dis.magnitude < senseDis && Input.GetKeyDown(KeyCode.F) && nKey>0)
        {
            //print("F");

            //PlayerPrefs.SetInt("Pouch", nKey-1);
            nKey -= 1;
            player.GetComponent<SR_PlayerInventory>().numberOfKeys = nKey;
            if (door.transform.rotation.z >= 0 && door.transform.rotation.z < 90) opening = true;
            
        }

        if (door.transform.rotation.y >= 0.2f)
        {
            
            opening = false;
        }

        if (opening == true)
        {
            door.transform.RotateAround(gameObject.transform.position, new Vector3(0, 1, 0), 50f * Time.deltaTime);
        }
    }
    
}