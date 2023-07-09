using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UnitBase))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float speedBuffMultiplier = 2f;
    [SerializeField] private float speedDebuffMultiplier = 0.5f;

    [SerializeField] private List<Transform> MovementPoints;
    [SerializeField] private float RequiredDistanceSquare = 0.00001f;
    [SerializeField] private float currentSpeed;
    public Action OnPrelastPosition;

    private UnitBase unitBase;
    private int currentPointIndex = 0;

    private void Start()
    {
        unitBase = GetComponent<UnitBase>();
        currentSpeed = unitBase.unitData.NormalSpeed;

        GetComponent<UnitTags>().OnTagsChanged += ToggleSpeedEffects;
        OnPrelastPosition += AddMoneyForProgress;
    }

    private void Update()
    {
        if (MovementPoints.Count > 0)
        {
            MoveUnit();

            TryChangeIndex();

            transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * currentSpeed * .75f);
        }
    }

    private void ToggleSpeedEffects(UnitTypes tag, bool isOn)
    {
        if (tag == UnitTypes.Slowness)
        {
            if (isOn)
                currentSpeed = unitBase.unitData.NormalSpeed * speedDebuffMultiplier;
            else
                currentSpeed = unitBase.unitData.NormalSpeed / speedDebuffMultiplier;
        }

        if (tag == UnitTypes.SpedUp)
        {
            if (isOn)
                currentSpeed = unitBase.unitData.NormalSpeed * speedBuffMultiplier;
            else
                currentSpeed = unitBase.unitData.NormalSpeed / speedBuffMultiplier;
        }

    }

    private void MoveUnit()
    {
        if(MovementPoints.Count < 1)
            return;
        transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * currentSpeed * .75f);
    }

    private void TryChangeIndex()
    {
        
        if (currentPointIndex > MovementPoints.Count - 2)
            OnPrelastPosition.Invoke();

        if (IsNearPoint() && currentPointIndex < MovementPoints.Count - 1)
            currentPointIndex++;
    }

    private bool IsNearPoint()
    {
        var distance = MovementPoints[currentPointIndex].position - transform.position;

        var distanceSqr = distance.sqrMagnitude;

        return distanceSqr <= RequiredDistanceSquare;
    }

    private void AddMoneyForProgress()
        => PlayerData.Money += 30 * RoundManager.RoundCount;

    public void SetWaypoints(List<Transform> waypoints)
    {
        MovementPoints = waypoints;
    }
}
