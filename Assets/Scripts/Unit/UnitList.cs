using System;

public static class UnitList
{
    private static uint _UnitRoundCount;

    public static uint UnitRoundCount
    {
        get => _UnitRoundCount;
        private set
        {
            _UnitRoundCount = value;
            OnUnitRoundCountChange?.Invoke();
        } 
    }

    public static Action OnUnitRoundCountChange;

    public static void ResetCount()
        => UnitRoundCount = 0;

    public static void SetCount(uint count)
        => UnitRoundCount = count;

    public static void AddObject()
    {
        //UnitRoundCount++;
    }

    public static void RemoveObject()
        => UnitRoundCount--;
}
