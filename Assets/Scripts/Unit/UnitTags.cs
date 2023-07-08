using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTags : MonoBehaviour, IEnumerable
{
    [SerializeField] private List<UnitTypes> thisUnitTags;
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
        return true;
    }

    public void RemoveTag(UnitTypes tag)
    {
        thisUnitTags.Remove(tag);
    }

    private void RemoveQueuedTag()
    {
        RemoveTag(deleteQueue.Dequeue());
    }

    public void AddRemporarTag(UnitTypes tag, float time)
    {
        if (!AddTag(tag))
            return;

        deleteQueue.Enqueue(tag);
        Invoke("RemoveQueuedTag", time);
    }
}