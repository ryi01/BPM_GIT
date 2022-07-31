using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_WeaponPickUp : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    

    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }
}
