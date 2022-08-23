using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //<- 이거 필수 입력


public class menutogame : MonoBehaviour
{

    public void Next()
    {
        LoadingSceneManager.LoadScene("TotalMap");
    }
    
}
