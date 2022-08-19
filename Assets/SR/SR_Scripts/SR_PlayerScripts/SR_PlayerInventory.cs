using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_PlayerInventory : MonoBehaviour
{
    public int numberOfCoins { get; set; }
    public int numberOfKeys { get; set; }
    public int numberOfScrolls { get; set; }


    public Text keyText;
    public Text coinText;


    public void CoinCollected()
    {
        numberOfCoins++;
        //PlayerPrefs.SetInt("Wallet", numberOfCoins);

    }
    public void KeyCollected()
    {
        numberOfKeys++;
       // PlayerPrefs.SetInt("Pouch", numberOfKeys);
    }

    public void ScrollCollected()
    {
        numberOfScrolls++;
        //PlayerPrefs.SetInt("Skill", numberOfScrolls);
    }

    void Update()
    {
        int c = numberOfCoins; // PlayerPrefs.GetInt("Wallet");
        int k = numberOfKeys; // PlayerPrefs.GetInt("Pouch");
        keyText.text = k + " ";
        coinText.text = c + " ";
    }
}
