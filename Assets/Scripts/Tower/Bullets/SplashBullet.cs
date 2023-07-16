using System;
using UnityEngine;

namespace Towers
{
    public class SplashBullet : Bullet
    {
        [SerializeField] private float splashRadius;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject != target)
                return;
            
            foreach (var hit in CheckSplashRadius())
            {
                hit.collider.GetComponent<UnitBase>().HP.DealDamage(Damage);
            }
        }

        private RaycastHit2D[] CheckSplashRadius() 
            => Physics2D.CircleCastAll(transform.position, splashRadius, Vector2.zero, 0, unitLayer);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, splashRadius);
        }
    }
}