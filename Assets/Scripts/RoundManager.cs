using System;
using UnityEngine;

public class RoundManager
{
    public static Action OnRoundStart;
    public static Action OnRoundEnd;

    public static Action OnGameOver;

    public static int RoundCount { get; private set; } = 0;
    
    public static void StartNewRound()
    {
        Debug.Log(UnitList.UnitRoundCount);
        Debug.Log(UnitList.UnitRoundCount > 0);

        if (UnitList.UnitRoundCount > 0)
            return;

        RoundCount++;
        OnRoundStart();
    }
}
