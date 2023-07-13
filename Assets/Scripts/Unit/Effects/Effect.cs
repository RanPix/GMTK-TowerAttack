using UnityEngine;

namespace AI.Unit.Effects
{
    public enum EffectType
    {
        Slowing,
        Running,
    }

    [CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect")]
    public class Effect : ScriptableObject
    {
        public float effectDuration;
        public EffectType effectType;
    }
}