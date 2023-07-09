using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitTags))]
public class Health
{
    public float Current { get; private set; }
    public float Max { get; private set; }
    public float HealthPercent
        => Current / Max;

    public Action Death;
    private UnitTags parentTags;
    private List<CountAmountFor> counters = new();
    private delegate float CountAmountFor(Damage damage);

    public Health(UnitBase parent)
    {
        Max = parent.unitData.MaxHP;
        Current = Max;

        parentTags = parent.GetComponent<UnitTags>();
        AddTagCounters();

        parentTags.OnTagsChanged += (_, _)
            => AddTagCounters();
    }

    private void AddTagCounters()
    {
        foreach (var tag in parentTags)
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
            Kill();
    }

    public void Heal(float amount)
    {
        Current += amount;

        if (Current > Max)
            Current = Max;
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

    public void Kill()
    {
        Death.Invoke();
    }
}
