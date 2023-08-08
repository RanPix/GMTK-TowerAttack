using Assets.Scripts.Unit.UnitTags;

namespace Assets.Scripts.Tower.Damage
{
    [System.Serializable]
    public struct DamageModifiers
    {
        public UnitStatus type;
        public float DamageModifier;
    }
}