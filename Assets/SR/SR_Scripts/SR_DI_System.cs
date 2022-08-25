using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_DI_System : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SR_DamageIndicator indicatorPrefab = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;

    private Dictionary<Transform, SR_DamageIndicator> Indicators = new Dictionary<Transform, SR_DamageIndicator>();

    #region Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObhectInSight = null;
    #endregion

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObhectInSight += InSight;
    }
    private void OnDisable()
    {
        CreateIndicator += Create;
        CheckIfObhectInSight += InSight;
    }
    void Create(Transform target)
    {
        if(Indicators.ContainsKey(target))
        {
            Indicators[target].Restart();
            return;
        }
        SR_DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
        newIndicator.Register(target, player, new Action(() => { Indicators.Remove(target); }));

        Indicators.Add(target, newIndicator);
    }
    bool InSight(Transform t)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
