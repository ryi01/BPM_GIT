using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SR_BossToEnding : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            SceneManager.LoadScene(7);

        }
    }
}

