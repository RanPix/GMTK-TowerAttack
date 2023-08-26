using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Unit.TagSystem;

[RequireComponent(typeof(UnitBase))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float speedBuffMultiplier = 2f;
    [SerializeField] private float speedDebuffMultiplier = 0.5f;
    [Space]
    [SerializeField] private List<Transform> MovementPoints;
    [Space]
    [SerializeField] private float RequiredDistanceSquare = 0.00001f;
    [SerializeField] private float currentSpeed;

    private Transform rotationDirection;

    private int currentPointIndex = 0;

    private float rotationSpeed = 6f;

    public Action OnPrelastPosition;

    private UnitBase unitBase;

    private void Start()
    {
        unitBase = GetComponent<UnitBase>();
        currentSpeed = unitBase.unitData.NormalSpeed;
        
        unitBase.Tags.OnTagsChanged += ToggleSpeedEffects;
        OnPrelastPosition += AddMoneyForProgress;
    }

    private void Update()
    {
        if (MovementPoints.Count <= 0)
            return;

        MoveUnit();

        TryChangeIndex();

        Rotate();

        transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * currentSpeed * .75f);
    }

    private void Rotate()
    {
        if(transform.rotation != rotationDirection.rotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection.rotation, Time.deltaTime * currentSpeed * rotationSpeed);
    }

    private void ToggleSpeedEffects(UnitStatus tag, bool isOn)
    {
        if (tag == UnitStatus.Slowed)
        {
            if (isOn)
                currentSpeed *= speedDebuffMultiplier;
            else
                currentSpeed /= speedDebuffMultiplier;
                
        }

        if (tag == UnitStatus.SpedUp)
        {
            if (isOn)
                currentSpeed *= speedBuffMultiplier;
            else
                currentSpeed /= speedBuffMultiplier;
        }
    }

    private void MoveUnit()
        => transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * currentSpeed * .75f);

    private void TryChangeIndex()
    {
        if (currentPointIndex > MovementPoints.Count - 2)
            OnPrelastPosition.Invoke();

        if (IsNearPoint() && currentPointIndex < MovementPoints.Count - 1)
            ChangeWaypoint();
    }

    private void ChangeWaypoint()
    {
        rotationDirection = MovementPoints[currentPointIndex];

        currentPointIndex++;
    }

    private bool IsNearPoint()
    {
        var distance = MovementPoints[currentPointIndex].position - transform.position;

        var distanceSqr = distance.sqrMagnitude;

        return distanceSqr <= RequiredDistanceSquare;
    }

    private void AddMoneyForProgress()
        => PlayerData.Money += 30 * RoundManager.Instance.RoundCount;

    public void SetWaypoints(List<Transform> waypoints)
    {
        MovementPoints = waypoints;

        rotationDirection = MovementPoints[0];
    }
}
