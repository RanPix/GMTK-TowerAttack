using System;
using Mirror;
using UnityEngine;

public class Gate : NetworkBehaviour
{
    public static Gate Instance { get; private set; }

    [SerializeField, SyncVar] private int gateHealth = 100;
    [property: SerializeField] public int GateHealth
    {
        get => gateHealth;
        
        [Server]
        private set
        {
            gateHealth = value;

            if (gateHealth <= 0)
            {
                gateHealth = 0;
                OnVictory?.Invoke();
            }

            OnGateHealthChanged?.Invoke();
        }
    }


    public Action OnGateHealthChanged;
    public Action OnVictory;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Gate instance already exists");
    }

    [Server]
    public void DamageGate(int damage)
    => GateHealth -= damage;
}
