using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_PlayerInventory : MonoBehaviour
{
    public int numberOfCoins { get; private set; }
    public int numberOfKeys { get; private set; }

    public Text keyText;
    public Text coinText;

    public void CoinCollected()
    {
        numberOfCoins++;
    }
    public void KeyCollected()
    {
        numberOfKeys++;
    }
    void Update()
    {
        keyText.text = numberOfKeys + " ";
        coinText.text = numberOfCoins + " ";
    }
}
