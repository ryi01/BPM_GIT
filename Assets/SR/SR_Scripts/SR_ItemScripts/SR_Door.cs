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

    private void Update()
    {
        door = GameObject.Find("Door");
        player = GameObject.Find("Player").transform;
        int pouch = PlayerPrefs.GetInt("Pouch");

        SR_PlayerInventory playerInventory = player.GetComponent<SR_PlayerInventory>();

        dis = player.position - gameObject.transform.position;



        if (dis.magnitude < senseDis && Input.GetKeyDown(KeyCode.F)/* && pouch > 0*/)
        {
            print("F");
            if (playerInventory != null)
            {
                PlayerPrefs.SetInt("Pouch", pouch - 1);
                opening = true;
            }
        }

        if (door.transform.rotation.z >= 90) opening = false;

        if(opening == true)
        {
            door.transform.RotateAround(gameObject.transform.position, new Vector3(0, 1, 0), 20f * Time.deltaTime);
        }
    }
}