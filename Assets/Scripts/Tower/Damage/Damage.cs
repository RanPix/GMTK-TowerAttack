using System;

namespace Assets.Scripts.Tower.DamageSystem
{
    [Serializable]
    public struct Damage
    {
        public float amount;
        public DamageType type;

        public static Damage operator *(Damage damage, float modifier)
        {
            damage.amount *= modifier;

            return damage;
        }

        public static implicit operator float(Damage damage)
            => damage.amount;

        public override string ToString()
            => $"{amount} {type}";
    }
}