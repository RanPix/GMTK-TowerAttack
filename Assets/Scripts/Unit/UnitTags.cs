using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTags : MonoBehaviour, IEnumerable
{
    [SerializeField] private List<UnitTypes> thisUnitTags;

    public bool ContainsUnitTag(UnitTypes requestedTag)
        => thisUnitTags.Contains(requestedTag);

    public IEnumerator GetEnumerator()
        => thisUnitTags.GetEnumerator();
}
