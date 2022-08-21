using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private SR_PlayerMove pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    private float currentTime = 0;
    public Text curTime;

    public Image redCenter;
    public Image dashImage;
    float fillAmount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<SR_PlayerMove>();
        redCenter.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        curTime.text = currentTime + " ";
        if (currentTime > 0.3409f ) currentTime -= 0.3409f;
    }

    private void Update()
    {
        if (GameManager.Instance.m_state != GameManager.GameState.Playing)
        {
            return;
        }
        fillAmount = dashImage.fillAmount;
        if (fillAmount >= 1) fillAmount = 1;
        fillAmount += 1/0.6818f*Time.deltaTime;

        if (Input.GetKeyDown(dashKey))
        {
            if ((currentTime > 0 && currentTime < 0.15f) || (currentTime > 0.1909f && currentTime < 0.3409f)) Dash();
            else StartCoroutine(Blink());

            
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(dashKey)) pm.dashing = false;

        if (dashCdTimer > 0) dashCdTimer -= Time.deltaTime;

        dashImage.fillAmount = fillAmount;
    }

    private void Dash()
    {
        
        
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd; fillAmount = 0;

        pm.dashing = true;

        Vector3 forceToApply = orientation.forward * dashForce+ orientation.up * dashUpwardForce;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);

    }
    
    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.VelocityChange);
    }

    private void ResetDash()
    {
        pm.dashing = false;
    }

    IEnumerator Blink()
    {
        redCenter.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3409f);
        redCenter.gameObject.SetActive(false);

    }

}
