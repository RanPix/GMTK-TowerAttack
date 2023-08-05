using System;
using Mirror;

public class RoundManager : NetworkBehaviour
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

    [field: SyncVar]
    public int RoundCount { get; private set; }
    
    [field: SyncVar]
    public bool IsMidRound { get; private set; }

    public const int MaxWaveCount = 25;

    public Action OnRoundStart;
    public Action OnRoundEnd;

    public Action OnGameOver;

    private RoundManager()
    { 
        Init();
        GameDataProcessor.Instance.ResetMoneyForAllPlayers();
    }
    
    [Server]
    private void Init()
        => UnitList.OnUnitRoundCountChange += CheckIfRoundEnded;
    
    [Server]
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

        GameDataProcessor.Instance.AddMoneyForAllPlayers(15);
    }
    
    [Server]
    private void CheckIfRoundEnded()
    {
        if (UnitList.UnitRoundCount > 0)
            return;

        IsMidRound = false;
        OnRoundEnd?.Invoke();
    }
    
    [Server]
    public void Reset()
        => _instance = new();
}
