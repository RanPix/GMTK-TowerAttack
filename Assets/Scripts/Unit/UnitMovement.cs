using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitBase))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementPoints;
    [SerializeField] private float RequiredDistanceSquare = 0.00001f;
    [SerializeField] private float Speed;
    public Action OnPrelastPosition;

    private int currentPointIndex = 0;

    private void Start()
    {
        var unitBase = GetComponent<UnitBase>();
        Speed = unitBase.unitData.Speed;

        OnPrelastPosition += AddMoneyForProgress;
    }

    private void Update()
    {
        if (MovementPoints.Count > 0)
        {
            MoveUnit();

            TryChangeIndex();

            transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * Speed * .75f);
        }
    }

    private void MoveUnit()
    {
        if(MovementPoints.Count < 1)
            return;
        transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * Speed * .75f);
    }

    private void TryChangeIndex()
    {
        if (currentPointIndex > MovementPoints.Count - 2)
            OnPrelastPosition.Invoke();

        if (IsNearPoint() && currentPointIndex < MovementPoints.Count - 1)
            ChangeWaypoint();
    }

    private void ChangeWaypoint()
    {
        transform.rotation = MovementPoints[currentPointIndex].rotation;

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
