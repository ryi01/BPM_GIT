using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    float currentTime = 0;
    private void OnTriggerStay(Collider other)
    {
        currentTime += Time.deltaTime;
        if (currentTime > 1.0f) Destroy(gameObject);
    }
}
