using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTags : MonoBehaviour, IEnumerable
{
    [SerializeField] private List<UnitTypes> thisUnitTags;
    
    private Queue<UnitTypes> deleteQueue = new();
    
    public Action<UnitTypes, bool> OnTagsChanged;

    public bool ContainsUnitTag(UnitTypes requestedTag)
        => thisUnitTags.Contains(requestedTag);

    public IEnumerator GetEnumerator()
        => thisUnitTags.GetEnumerator();

    public bool AddTag(UnitTypes tag, bool canStack = true)
    {
        if (thisUnitTags.Contains(tag) && !canStack)
            return false;

        thisUnitTags.Add(tag);
        OnTagsChanged?.Invoke(tag, true);

        return true;
    }

    public void RemoveTag(UnitTypes tag)
    {
        OnTagsChanged?.Invoke(tag, false);
        thisUnitTags.Remove(tag);
    }

    private void RemoveQueuedTag()
    {
        RemoveTag(deleteQueue.Dequeue());
    }

    public void AddTemporarTag(UnitTypes tag, float time, bool canStack = true)
    {
        if (!AddTag(tag, canStack))
            return;

        deleteQueue.Enqueue(tag);
        Invoke(nameof(RemoveQueuedTag), time);
    }
}