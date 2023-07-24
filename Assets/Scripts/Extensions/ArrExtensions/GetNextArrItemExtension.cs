using UnityEngine;
using System;
using System.Linq;

public static class GetNextArrItemExtension
{
    public static T GetNextItem<T>(this T[] array, T currentItem)
    {
        if(!array.Contains(currentItem))
            throw new ArgumentException();
        
        return array[(Array.IndexOf(array, currentItem) + 1) % array.Length];
    }
}
