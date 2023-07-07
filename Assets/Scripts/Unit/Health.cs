using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float Current { get; private set; }
    [SerializeField] public float Max { get; private set; }
    public Action Death;
    
    public void Awake()
    {
        Current = Max;
    }

    public void DealDamage(float amount)
    {
        Current -= amount;

        if (Current < 0)
            Death.Invoke();
    }
}
