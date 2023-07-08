using UnityEditor;
using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "TowerAbilitySettings", menuName = "ScriptableObjects/TowerAbilitySettings")]
    public class TowerAbilitySettings : ScriptableObject
    { 
        public TowerAbility towerAbility;

        public float CooldownRate;
        
    }
}