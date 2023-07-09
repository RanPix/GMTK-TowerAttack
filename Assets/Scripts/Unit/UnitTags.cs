using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTags : MonoBehaviour, IEnumerable
{
    [SerializeField] private List<UnitTypes> thisUnitTags;

    [HideInInspector] public Action<UnitTypes, bool> OnTagsChanged;
    private Queue<UnitTypes> deleteQueue;

    public bool ContainsUnitTag(UnitTypes requestedTag)
        => thisUnitTags.Contains(requestedTag);

    public IEnumerator GetEnumerator()
        => thisUnitTags.GetEnumerator();

    public bool AddTag(UnitTypes tag)
    {
        if (thisUnitTags.Contains(tag))
            return false;

        thisUnitTags.Add(tag);
        OnTagsChanged?.Invoke(tag, true);
        return true;
    }

    public void RemoveTag(UnitTypes tag)
    {
        OnTagsChanged?.Invoke(tag, true);
        thisUnitTags.Remove(tag);
    }

    private void RemoveQueuedTag()
    {
        RemoveTag(deleteQueue.Dequeue());
    }

    public void AddTemporarTag(UnitTypes tag, float time)
    {
        if (!AddTag(tag))
            return;

        deleteQueue.Enqueue(tag);
        Invoke("RemoveQueuedTag", time);
    }
}