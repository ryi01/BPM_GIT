using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �뽬�� ���� ��, UI�� Ȯ���� => 0.00055, 0.00055
public class UIShake : MonoBehaviour
{
    public enum Shake
    {
        Normal,
        ZoomIn,
        ShakeCam,
        ZoomOut,
    }

    // �뽬�� ������ �Ƚ����� �˾ƺ���
    GameObject player;
    // UI RectTransform��������
    RectTransform ui;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ui = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.m_state != GameManager.GameState.Playing)
        {
            return;
        }
        UINormal();

        if (player.GetComponent<SR_PlayerMove>().dashing)
        {
            UIZoomIn();
        }
/*
        if (Input.GetButtonDown("Jump"))
        {
            Shaking();
        }
*/
        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.R))
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
        StopCoroutine(JumpUI());
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
        while(currentTime >= -0.02f)
        {
            print(currentTime);
            currentTime -= 0.005f;
            ui.anchoredPosition = new Vector2(0, currentTime);
            yield return null;

        }
        ui.anchoredPosition = new Vector2(0, -0.02f);
        yield return new WaitForSeconds(0.001f);
        while (currentTime < 0 && currentTime > 0.02f)
        {
            currentTime += 0.005f;
            ui.anchoredPosition = new Vector2(0, currentTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.001f);
        ui.anchoredPosition = new Vector2(0, 0);
    }

}
