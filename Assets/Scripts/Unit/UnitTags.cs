using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTags : MonoBehaviour, IEnumerable
{
    [SerializeField] private List<UnitTypes> thisUnitTags;

    public bool ContainsUnitTag(UnitTypes requestedTag)
    {
        foreach(var unitTag in thisUnitTags)
        {
            if(unitTag == requestedTag)
                return true;
        }

        return false;
    }

    public IEnumerator GetEnumerator()
        => thisUnitTags.GetEnumerator();
}
