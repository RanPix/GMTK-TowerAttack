using UnityEditor;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "TowerAbilitySettings", menuName = "ScriptableObjects/TowerAbilitySettings")]
    public class TowerAbilitySettings : ScriptableObject
    { 
        public TowerAbility towerAbility;

        public float CooldownRate;
        
    }
}