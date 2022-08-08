using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SR_Enemy2ToLibrary : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            SceneManager.LoadScene("4 LibraryScene");

        }
    }
}

