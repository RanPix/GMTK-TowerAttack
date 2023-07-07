using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitTags))]
public class Health : MonoBehaviour
{
    public float Current { get; private set; }
    [SerializeField] public float Max { get; private set; }
    public Action Death;
    private List<CountAmountFor> counters = new();
    private delegate float CountAmountFor(Damage damage);

    public void Awake()
    {
        Current = Max;
        AddTagCounters();
    }

    private void AddTagCounters()
    {
        var tags = GetComponent<UnitTags>();

        foreach (var tag in tags)
        {
            switch (tag)
            {
                case UnitTypes.Light:
                    counters.Add(CountForLight);
                    break;

                case UnitTypes.Heavy:
                    counters.Add(CountForHeavy);
                    break;

                case UnitTypes.Organic:
                    counters.Add(CountForOrganic);
                    break;

                default:
                    break;
            }
        }
    }

    public void DealDamage(Damage damage)
    {

        Current -= CountAmount(damage);
        if (Current < 0)
            Death.Invoke();
    }

    private float CountAmount(Damage damage)
    {
        foreach (var counter in counters)
        {
            damage.amount = counter(damage);
        }

        return damage.amount;
    }

    public float CountForLight(Damage damage)
    {
        float amount = damage.amount;

        amount *= 1.1f;

        switch (damage.type)
        {
            default:
                break;
        }

        return amount;
    }

    public float CountForHeavy(Damage damage)
    {
        float amount = damage.amount;

        switch (damage.type)
        {
            case DamageType.Blank:
                amount *= 0.9f;
                break;

            default:
                break;
        }

        return amount;
    }

    public float CountForOrganic(Damage damage)
    {
        float amount = damage.amount;

        switch (damage.type)
        {
            default:
                break;
        }

        return amount;
    }
}
