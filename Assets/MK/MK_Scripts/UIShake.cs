using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �뽬�� ���� ��, UI�� Ȯ��� => 0.00055, 0.00055
public class UIShake : MonoBehaviour
{
    public enum Shake
    {
        Normal,
        ZoomIn,
        ZoomIn1,
        ZoomOut,
    }

    // �뽬�� ����� �Ƚ���� �˾ƺ���
    SR_PlayerMove player;
    SR_Pistol pistol;
    // UI RectTransform��������
    RectTransform ui;
    // �뽬�� ������ �����ϴ� ī��Ʈ
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
