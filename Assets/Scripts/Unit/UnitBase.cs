using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [field : SerializeField] public Health UnitHealth { get; private set; }

    [field : SerializeField] public UnitTags UnitTags { get; private set; }

    [field: SerializeField] public float Speed = 1f;

}
