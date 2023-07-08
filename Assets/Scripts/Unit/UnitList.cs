using System;

public static class UnitList
{
    public static Action LostAllUnits;
    private static uint unitCount;

    public static void ResetCount()
    {
        unitCount = 0;
    }

    public static void AddObject()
    {
        unitCount++;
    }

    public static void RemoveObject()
    {
        if (--unitCount < 1)
            LostAllUnits?.Invoke();
    }
}
