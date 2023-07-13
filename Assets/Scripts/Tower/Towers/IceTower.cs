using UnityEngine;

namespace Towers
{
    //Уолтер убери свой скрипт, Уолтер
    public class IceTower : Tower
    {
        [SerializeField] private GameObject slowMotionField;

        private void Update()
        {
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, attackRadius);
        }
    }
}