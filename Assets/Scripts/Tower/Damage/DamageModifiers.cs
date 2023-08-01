using Assets.Scripts.Unit.UnitTypes;

namespace Assets.Scripts.Tower.Damage
{
    [System.Serializable]
    public struct DamageModifiers
    {
        public UnitStatus type;
        public float DamageModifier;
    }
}