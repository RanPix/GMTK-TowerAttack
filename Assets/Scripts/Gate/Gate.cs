using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static Gate Instance { get; private set; }

    [property: SerializeField] public int GateHealth
    {
        get => gateHealth;
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

    [SerializeField] private int gateHealth = 100;

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

    public void DamageGate(int damage)
    => GateHealth -= damage;
}
