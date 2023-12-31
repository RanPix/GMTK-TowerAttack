﻿using Assets.Scripts.Unit.TagSystem;

namespace Assets.Scripts.Tower.DamageSystem
{
    [System.Serializable]
    public struct DamageModifier
    {
        public UnitStatus type;
        public float Modifier;

        public static implicit operator float(DamageModifier modifier)
            => modifier.Modifier;
    }
}