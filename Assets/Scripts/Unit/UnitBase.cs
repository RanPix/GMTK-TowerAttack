using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [field : SerializeField] public Health UnitHealth { get; private set; }

    [field : SerializeField] public UnitTags UnitTags { get; private set; }

    [field: SerializeField] public float Speed = 1f;

    private void Update()
    {
        var thing = Physics2D.OverlapCircle(new Vector2(1, 1), 2);
    }
}
