using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 대쉬를 썼을 때, UI가 확대됨 => 0.00055, 0.00055
public class UIShake : MonoBehaviour
{
    public enum Shake
    {
        Normal,
        ZoomIn,
        ShakeCam,
        ZoomOut,
    }

    // 대쉬를 썼는지 안썼는지 알아보기
    GameObject player;
    // UI RectTransform가져오기
    RectTransform ui;

    public float sinSpeed = 2;
    public float amp = 0.5f;
    Vector3 origin;
    float theta = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ui = GetComponent<RectTransform>();
        origin = new Vector3(0, 0, 0.49f);
    }

    // Update is called once per frame
    void Update()
    {
        UINormal();

        if (player.GetComponent<SR_PlayerMove>().dashing)
        {
            UIZoomIn();
        }

    }

    private void UINormal()
    {
        StopCoroutine(ZoomUI());
        ui.localScale = new Vector3(0.0005f, 0.0005f, 0);
    }

    void UIZoomIn()
    {
        StartCoroutine(ZoomUI());
    }  

    public void Shaking()
    {
        StartCoroutine(JumpUI());
    }

    IEnumerator ZoomUI()
    {
        float currentTime = 0;

        while (currentTime < 0.00003f)
        {
            currentTime += 0.000005f;
            ui.localScale += new Vector3(currentTime, currentTime, 0);
            yield return null;
        }
    }
    IEnumerator JumpUI()
    {
        float currentTime = 0;
        while(currentTime < 0.01f)
        {
            currentTime += 0.002f;
            ui.anchoredPosition += new Vector2(0, currentTime);
            yield return null;

        }
        yield return new WaitForSeconds(0.001f);
        while (currentTime > 0) 
        {
            currentTime -= 0.002f;
            ui.anchoredPosition -= new Vector2(0, currentTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.001f); 
        ui.anchoredPosition = new Vector2(0, 0);
    }  

}
