using UnityEngine;

namespace Towers
{
    public class IceTower : Tower
    {
        [SerializeField] private GameObject slowMotionField;

        private void Update()
        {
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, attackRadius);
        }
    }
}