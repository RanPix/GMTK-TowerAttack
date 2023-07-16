using System;

public class RoundManager
{
    private static RoundManager _instance;

    public static RoundManager Instance 
    {
        get
        {
            if (_instance == null)
                _instance = new();

            return _instance;
        }
    }

    public int RoundCount { get; private set; } = 0;

    public bool IsMidRound { get; private set; } = false;

    public const int MaxWaveCount = 25;

    public Action OnRoundStart;
    public Action OnRoundEnd;

    public Action OnGameOver;

    private RoundManager()
    { 
        Init();
        PlayerData.ResetMoney();
    }
    
    private void Init()
        => UnitList.OnUnitRoundCountChange += CheckIfRoundEnded;

    public void StartNewRound()
    {
        if (UnitList.UnitRoundCount > 0)
            return;

        IsMidRound = true;

        RoundCount++;

        if (RoundCount > MaxWaveCount && Gate.Instance.GateHealth > 0)
        {
            OnGameOver();
            return;
        }

        OnRoundStart?.Invoke();

        PlayerData.Money += 15;
    }

    private void CheckIfRoundEnded()
    {
        if (UnitList.UnitRoundCount > 0)
            return;

        IsMidRound = false;
        OnRoundEnd?.Invoke();
    }

    public void RESET()
        => _instance = new();
}
