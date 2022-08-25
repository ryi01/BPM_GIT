using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_IndicatorFactory : MonoBehaviour
{
    [Range(5, 30)]
    [SerializeField] float destoryTimer = 15.0f;

    void Start()
    {
        Invoke("Register", Random.Range(0, 8));
    }
    void Register()
    {
        if(!SR_DI_System.CheckIfObhectInSight(this.transform))
        {
            SR_DI_System.CreateIndicator(this.transform);
        }
        Destroy(this.gameObject, destoryTimer);
    }
}
