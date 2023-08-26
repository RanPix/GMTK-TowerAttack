using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace Assets.Scripts.Tower.DamageSystem
{
    public class DamageModifiersCollection : MonoBehaviour
    {
        private static DamageModifiersCollection _instance;

        [SerializedDictionary("DamageType", "Modifiers")]
            public SerializedDictionary<DamageType, DamageModifier[]> Modifiers;

        private void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (_instance == null)
                _instance = this;
            else
                Debug.LogWarning("Damage modifier container already exists");
        }

        public static DamageModifier[] GetDamageModifiers(DamageType type)
            => _instance.Modifiers[type];

        public static DamageModifier[] GetDamageModifiers(Damage damage)
            => GetDamageModifiers(damage.type);
    }
}
