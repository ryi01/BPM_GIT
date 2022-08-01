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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<SR_PlayerMove>();
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        curTime.text = currentTime + " ";
        if (currentTime > 0.3375) currentTime = 0;

    }
    private void Update()
    {


        if (Input.GetKeyDown(dashKey) && ((currentTime > 0 && currentTime < 0.15f) || (currentTime > 0.1875f && currentTime < 0.3375f))) Dash();

        if (dashCdTimer > 0) dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;

        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);

    }
    
    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;
    }

    
}
