using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_DamageIndicator : MonoBehaviour
{
    const float maxTimer = 8.0f;
    float timer = maxTimer;

    CanvasGroup canvasGroup = null;
    CanvasGroup CanvasGroup
    {
        get
        {
            if(canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if(canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    RectTransform rect = null;
    RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
                if (rect == null)
                {
                    rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return rect;
        }
    }

    public Transform Target { get; set; } = null;
    Transform player = null;

    IEnumerator IE_Countdown = null;
    private Action unRegister = null;

    Quaternion tRot = Quaternion.identity;
    Vector3 tPos = Vector3.zero;

    public void Register(Transform t, Transform player, Action unRegister)
    {
        this.Target = Target;
        this.player = player;
        this.unRegister = unRegister;

        StartCoroutine(RotateToTheTarget());
        StartTimer();
    }
    public void Restart()
    {
        timer = maxTimer;
        StartTimer();
    }
    private void StartTimer()
    {
        if(IE_Countdown != null) { StopCoroutine(IE_Countdown); }
        IE_Countdown = Countdown();
        StartCoroutine(IE_Countdown);
    }
    IEnumerator RotateToTheTarget()
    {
        while(enabled)
        {
            if(Target)
            {
                tPos = Target.position;
                tRot = Target.rotation;
            }
            Vector3 direction = player.position - tPos;

            tRot = Quaternion.LookRotation(direction);
            tRot.z = tRot.y;
            tRot.x = 0;
            tRot.y = 0;

            Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y);
            Rect.localRotation = tRot * Quaternion.Euler(northDirection);

            yield return null;
        }
    }

    private IEnumerator Countdown()
    {
        while(CanvasGroup.alpha < 1.0f)
        {
            CanvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }
        while(timer>0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        while(CanvasGroup.alpha > 0.0f)
        {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        unRegister();
        Destroy(gameObject);
    }
}
