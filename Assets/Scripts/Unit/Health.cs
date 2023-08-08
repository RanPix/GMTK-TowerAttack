using System;
using UnityEngine;
using Assets.Scripts.Unit.TagSystem;
using Assets.Scripts.Tower.DamageSystem;

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
    }

    private void AddTagCounters()
    {
        _counters = null;

        foreach (UnitStatus tag in _parentTags)
            ToggleTagInCounters(tag, true);
    }

    private void ToggleTagInCounters(UnitStatus tag, bool toggle)
    {
        switch (tag)
        {
            case UnitStatus.Light:
                ToggleTagCounter(CountForLight, toggle);
                break;

            case UnitStatus.Heavy:
                ToggleTagCounter(CountForHeavy, toggle);
                break;

            case UnitStatus.Organic:
                ToggleTagCounter(CountForOrganic, toggle);
                break;

            default:
                break;
        }
    }

    private void ToggleTagCounter(CountAmountFor func, bool toggle)
    {
        if (toggle)
            _counters += func;
        else
            _counters -= func;
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
        if (_counters == null)
            return damage.amount;

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
