using System.Collections.Generic;
using UnityEngine;

public class UnitTags : MonoBehaviour
{
    [field : SerializeField] public List<UnitsTags> ThisUnitTags { get; private set; } = new List<UnitsTags>();
}
