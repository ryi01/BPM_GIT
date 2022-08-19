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
        ZoomIn1,
        ZoomOut,
    }

    // 대쉬를 썼는지 안썼는지 알아보기
    SR_PlayerMove player;
    SR_Pistol pistol;
    // UI RectTransform가져오기
    RectTransform ui;
    // 대쉬가 들어오면 증가하는 카운트
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<SR_PlayerMove>();
        ui = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.dashing)
        {
            UIZoomIn();
        }
        else
        {
            UINormal();
        }
    }

    private void UINormal()
    {
        StopAllCoroutines();
        ui.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
    }

    private void UIZoomIn()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomUI(0.00007f));
    }

    private void UIZoomIn1()
    {
        StartCoroutine(ZoomUI(0.00009f));
    }
    IEnumerator ZoomUI(float n)
    {
        float currentTime = 0;

        while (currentTime < n)
        {
            currentTime += 0.000004f;
            ui.localScale += new Vector3(currentTime, currentTime, 0);
            yield return null;
        }
    }
}
