using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    float currentTime = 0;
    private void Update()
    {
        Destroy(gameObject,0.3f);
    }
}
