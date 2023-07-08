using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitBase))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> MovementPoints;

    [SerializeField] private float RequiredDistanceSquare = 4f;

    private int currentPointIndex = 0;

    private UnitBase unitBase;

    private void Start()
        => unitBase = GetComponent<UnitBase>();
    private void Update()
    {
        TryChangeIndex();

        MoveUnit();

        TryChangeIndex();
    }
    private void MoveUnit()
        => transform.position = Vector2.MoveTowards(transform.position, MovementPoints[currentPointIndex].position, Time.deltaTime* unitBase.Speed * .75f);
    private void TryChangeIndex()
    {
        if (currentPointIndex > MovementPoints.Count - 1)
            currentPointIndex = 0;

        if(IsNearPoint())
            currentPointIndex++;
    }
    private bool IsNearPoint()
    {
        var distance = MovementPoints[currentPointIndex].position - transform.position;

        var distanceSqr = distance.sqrMagnitude;

        return distanceSqr <= RequiredDistanceSquare;
    }
}
