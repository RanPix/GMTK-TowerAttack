using System;
using Assets.Scripts.Unit.TagSystem;
using Assets.Scripts.Tower.DamageSystem;

public class Health
{
    public float Current { get; private set; }
    public float Max { get; private set; }
    public float HealthPercent
        => Current / Max;

    private UnitTags _parentTags;

    public Action Death;

    public Health(UnitBase parent)
    {
        Max = parent.unitData.MaxHP;
        Current = Max;

        _parentTags = parent.Tags;
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
        var damageModifiers = DamageModifiersCollection.GetDamageModifiers(damage);

        foreach (var damageModifier in damageModifiers)
        {
            if (_parentTags.Contains(damageModifier.type))
                damage *= damageModifier;
        }

        return damage;
    }

    public void Kill()
        => Death.Invoke();
}
