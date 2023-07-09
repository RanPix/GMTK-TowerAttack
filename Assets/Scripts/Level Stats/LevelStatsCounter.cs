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
    [property: SerializeField] public int WaveNumber 
    {
        get => waveNumber;
        private set
        {
            if (value > MaxWave && GateHealth > 0)
                Lose?.Invoke();

            waveNumber = value;
            OnWaveChanged?.Invoke();
        }
    }
    public bool GameIsPaused { get; private set; } = true;

    public Action OnGateHealthChanged;
    public Action OnWaveChanged;
    public Action OnTimeChanged;

    public Action Lose;

    [SerializeField] private int gateHealth = 100;
    
    private int waveNumber = 1;
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

    public void IncreaseWave()
        => WaveNumber++;

    public void DamageGate(int damage)
        => GateHealth -= damage;
}
