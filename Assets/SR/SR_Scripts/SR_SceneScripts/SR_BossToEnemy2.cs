using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SR_BossToEnemy2 : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            SceneManager.LoadScene(5);

        }
    }
}

