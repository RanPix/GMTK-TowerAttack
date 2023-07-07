using System.Collections.Generic;
using UnityEngine;

public enum UnitsTags
{
    Light,
    Heavy,
    Organic,
}
public class UnitTags : MonoBehaviour
{
    [field : SerializeField] public List<UnitsTags> ThisUnitTags { get; private set; } = new List<UnitsTags>();

    public bool ContainsUnitTag(UnitsTags requestedTag)
    {
        foreach(var unitTag in ThisUnitTags)
        {
            if(unitTag == requestedTag)
                return true;
        }

        return false;
    }
}
