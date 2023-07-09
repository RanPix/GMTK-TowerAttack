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
            }

            OnGateHealthChanged?.Invoke();
        }
    }
    [property: SerializeField] public float PassedTime 
    { 
        get => passedTime; 
        private set
        {
            passedTime = value;
            OnTimeChanged?.Invoke();
        }
    }
    public bool GameIsPaused { get; private set; } = true;

    public Action OnGateHealthChanged;
    public Action OnTimeChanged;

    public Action OnVictory;

    public bool GameWon { get; private set; } = false;
    [SerializeField] private int gateHealth = 100;
    
    private float passedTime = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("WTF, YOU HAVE INSTANCE");
    }

    private void Update()
    {
        if(!GameIsPaused)
            PassedTime += Time.deltaTime;
    }

    public void TogglePause()
        => GameIsPaused = !GameIsPaused;

    public void DamageGate(int damage)
        => GateHealth -= damage;
}
