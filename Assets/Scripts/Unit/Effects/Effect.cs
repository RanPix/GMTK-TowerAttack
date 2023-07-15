using UnityEngine;

namespace AI.Unit.Effects
{
    public enum EffectType
    {
        Slowing,
        Running,
    }

    [CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Units/Effect")]
    public class Effect : ScriptableObject
    {
        public float effectDuration;
        public EffectType effectType;
    }
}