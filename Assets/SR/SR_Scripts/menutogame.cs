using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //<- �̰� �ʼ� �Է�


public class menutogame : MonoBehaviour
{

    public void Next()
    {
        LoadingSceneManager.LoadScene("TotalMap");
    }
    
}
