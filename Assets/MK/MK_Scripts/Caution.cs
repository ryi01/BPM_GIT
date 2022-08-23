using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caution : MonoBehaviour
{
    public GameObject butt;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        // 초반에 꺼져있음
        gameObject.SetActive(true);
    }

    float currentTime = 0;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 2)
        {
            currentTime = 0;
            gameObject.SetActive(false);
            butt.gameObject.SetActive(true);
        }
    }
}
