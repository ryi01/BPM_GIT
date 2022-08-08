using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SR_StartToEnemy1 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            SceneManager.LoadScene("5 EnemyScene1");

        }
    }
}

