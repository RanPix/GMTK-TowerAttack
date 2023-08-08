using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace Assets.Scripts.Tower.Damage
{
    public class DamageModifierCollection : MonoBehaviour
    {
        private static DamageModifierCollection Instance;

        [SerializedDictionary("DamageType", "Modifiers")]
            public SerializedDictionary<DamageType, DamageModifiers[]> Modifiers;

        private void Awake()
        {
            SetInstance();
        }

        private void SetInstance()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogWarning("Damage modifier container already exists");
        }

        public static DamageModifiers[] GetDamageModifiers(DamageType type)
            => Instance.Modifiers[type];
    }
}
