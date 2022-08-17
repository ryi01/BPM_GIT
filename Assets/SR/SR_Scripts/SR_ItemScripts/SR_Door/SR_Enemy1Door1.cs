using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Enemy1Door1 : MonoBehaviour
{
    GameObject door;
    public GameObject trigger;

    bool closing = false;

    private void Update()
    {
        int c = trigger.GetComponent<SR_StartToEnemy1>().cnt;
        door = GameObject.Find("E1_Door1");
        //print(door.transform.rotation.y);
        if (c == 1) closing = true;
        if (door.transform.rotation.y <= 0) closing = false;
        if (closing == true) door.transform.RotateAround(gameObject.transform.position, new Vector3(0, -1, 0), 50f * Time.deltaTime);
    }
}
