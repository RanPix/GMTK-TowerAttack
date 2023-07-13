using System;
using UnityEngine;

public class LevelStatsCounter : MonoBehaviour
{
    public static LevelStatsCounter Instance { get; private set; }

    [field: SerializeField] public int MaxWave { get; private set; }

    [property: SerializeField] public int GateHealth 
    { 
        get => gateHealth;
        private set
        {
            gateHealth = value;

            if (gateHealth <= 0) 
            {
                gateHealth = 0;
                GameWon = true;
                OnVictory?.Invoke();
                TogglePause();
            }

            OnGateHealthChanged?.Invoke();
        }
    }

    public bool GameIsPaused { get; private set; } = false;

    public Action OnGateHealthChanged;
    public Action OnTimeChanged;

    public Action OnVictory;

    public bool GameWon { get; private set; } = false;
    [SerializeField] private int gateHealth = 100;

    private void Awake()
    {
        RoundManager.Instance.OnGameOver += TogglePause;

        PlayerData.ResetMoney();

        Instantiate(PickedMap.map);

        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("WTF, YOU HAVE INSTANCE");
    }

    public void TogglePause()
    {
        GameIsPaused = !GameIsPaused;

        Time.timeScale = GameIsPaused ? 0 : 1;
    }

    public void DamageGate(int damage)
        => GateHealth -= damage;
}
