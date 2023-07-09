using UnityEngine;

public static class UnitList
{
    public static uint UnitRoundCount { get; private set; }

    public static void ResetCount()
    {
        UnitRoundCount = 0;
    }

    public static void SetCount(uint count)
    {
        UnitRoundCount = count;
    }

    public static void AddObject()
    {
        //UnitRoundCount++;
    }

    public static void RemoveObject()
    {
        if (--UnitRoundCount < 1)
            RoundManager.OnRoundEnd?.Invoke();
    }
}
