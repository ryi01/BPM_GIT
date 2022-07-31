using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_Item : MonoBehaviour
{
    int keyCnt = 0;
    int coinCnt = 0;
    public Text keyText;
    public Text coinText;
    
    void Start()
    {
        
    }

    void Update()
    {
        keyText.text = keyCnt + " ";
        coinText.text = coinCnt + " ";
    }
}
