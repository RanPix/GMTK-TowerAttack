using System;
using UnityEngine;

public class LevelStatsCounter : MonoBehaviour
{
    public static LevelStatsCounter Instance = null;

    [property: SerializeField] public float GateHealth 
    { 
        get => gateHealth; 
        private set=> OnGateHealthChanged.Invoke(); 
    }
    [property: SerializeField] public float PassedTime 
    { 
        get => passedTime; 
        private set => OnTimeChanged.Invoke(); 
    }

    [property: SerializeField] public int WaveNumber 
    {
        get => waveNumber;
        private set => OnWaveChanged.Invoke(); 
    }

    [field: SerializeField] public bool GameIsPaused { get; private set; } = false;

    public Action OnGateHealthChanged = () => { };
    public Action OnWaveChanged = () => { };
    public Action OnTimeChanged = () => { };

    private int waveNumber = 1;

    private float passedTime = 0;
    private float gateHealth = 100;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if(!GameIsPaused)
            passedTime += Time.deltaTime;
    }
    public void PauseTime()
        => GameIsPaused = true;

    public void IncreaseWave()
        => waveNumber++;
    public void DamageGate(float damage)
        => gateHealth -= damage;
}
