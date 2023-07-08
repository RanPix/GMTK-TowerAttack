using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitBase))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementPoints;
    [SerializeField] private float RequiredDistanceSquare = 4f;
    [SerializeField] private float Speed;
    public Action OnPrelastPosition = () => {};

    private int currentPointIndex = 0;

    private bool isAtTheGate = false;

    private void Start()
    {
        var unitBase = GetComponent<UnitBase>();
        Speed = unitBase.unitData.Speed;
    }

    private void Update()
    {
        MoveUnit();

        TryChangeIndex();

        transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * Speed * .75f);
    }

    private void MoveUnit()
        => transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime * Speed * .75f);

    private void TryChangeIndex()
    {
        if (currentPointIndex > MovementPoints.Count - 2)
            isAtTheGate = true;

        if (IsNearPoint() && currentPointIndex < MovementPoints.Count - 1)
            currentPointIndex++;
    }

    private bool IsNearPoint()
    {
        var distance = MovementPoints[currentPointIndex].position - transform.position;

        var distanceSqr = distance.sqrMagnitude;

        return distanceSqr <= RequiredDistanceSquare;
    }
}
