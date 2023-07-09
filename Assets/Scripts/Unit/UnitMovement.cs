using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UnitBase))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private UnitBase unitBase;
    
    [SerializeField] private List<Transform> MovementPoints;
    [SerializeField] private float RequiredDistanceSquare = 0.00001f;
    [SerializeField] private float currentSpeed;
    public Action OnPrelastPosition = () => {};

    private int currentPointIndex = 0;

    private void Start()
    {
        //unitBase = GetComponent<UnitBase>();
        currentSpeed = unitBase.unitData.NormalSpeed;
        GetComponent<UnitTags>().OnTagsChanged += ToggleSlowness;
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

    private void ToggleSlowness(UnitTypes tag, bool isOn)
    {
        if(tag != UnitTypes.Slowness)
            return;
        if (isOn)
        {
            currentSpeed = unitBase.unitData.NormalSpeed * 0.5f;
        }
        else
        {
            currentSpeed = unitBase.unitData.NormalSpeed;
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

    public void SetWaypoints(List<Transform> waypoints)
    {
        MovementPoints = waypoints;
    }
}
