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
        if (UnitList.UnitRoundCount > 0)
            return;

        RoundCount++;

        if (RoundCount > LevelStatsCounter.Instance.MaxWave && LevelStatsCounter.Instance.GateHealth > 0)
        {
            OnGameOver();
            return;
        }

        OnRoundStart?.Invoke();

        PlayerData.Money += 15;
    }

    public static void RESET()
    {
        RoundCount = 0;
        OnRoundStart = null;
        PlayerData.Money = 50;
    }
}
