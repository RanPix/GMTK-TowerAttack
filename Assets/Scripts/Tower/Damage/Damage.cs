using System;

namespace Assets.Scripts.Tower.DamageSystem
{
    [Serializable]
    public struct Damage
    {
        public float amount;
        public DamageType type;
    }
}