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

    private UnitTags _parentTags;
    private CountAmountFor _counters;
    private delegate float CountAmountFor(Damage damage);

    public Action Death;

    public Health(UnitBase parent)
    {
        Max = parent.unitData.MaxHP;
        Current = Max;

        _parentTags = parent.GetComponent<UnitTags>();
        AddTagCounters();

        _parentTags.OnTagsChanged += (_, _)
            => AddTagCounters();
    }

    private void AddTagCounters()
    {
        _counters = null;

        foreach (var tag in _parentTags)
        {
            switch (tag)
            {
                case UnitTypes.Light:
                    _counters += CountForLight;
                    break;

                case UnitTypes.Heavy:
                    _counters += CountForHeavy;
                    break;

                case UnitTypes.Organic:
                    _counters += CountForOrganic;
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
        foreach (CountAmountFor counter in _counters.GetInvocationList())
            damage.amount = counter.Invoke(damage);

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
        => Death.Invoke();
}
