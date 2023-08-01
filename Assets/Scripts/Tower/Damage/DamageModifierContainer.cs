using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace Assets.Scripts.Tower.Damage
{
    public class DamageModifierContainer : MonoBehaviour
    {
        public static DamageModifierContainer Instance { get; private set; }
        [SerializedDictionary("DamageType", "Modifiers")]
        public SerializedDictionary<DamageType, DamageModifiers[]> Modifiers { get; private set; }

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
    }
}
