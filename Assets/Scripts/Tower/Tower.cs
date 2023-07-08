using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tower 
{
    public class Tower: MonoBehaviour
    {
        [field: SerializeField] public int TowerTier { get; private set; }

        [SerializeField] private Bullet bullet;

        [SerializeField] private float attackRadius;
        [SerializeField] private float attackRate;
        private float attackTimer;

        [SerializeField] private Transform rotatablePart;

        [SerializeField] private GameObject target;
        public bool canAttack;
        

        public Vector2 position
        {
            get => new(transform.position.x, transform.position.y);
        }
        
        private void Start()
        {
            canAttack = true; 
        }

        private void Update()
        {
            attackTimer += Time.deltaTime;
            if (target)
            {
                if (((Vector2)target.transform.position - position).sqrMagnitude > attackRadius*attackRadius)
                {
                    target = null;
                    if(!GetTarget())
                        return;
                }

                
                rotatablePart.LookAt(target.transform);
                TryAttack();
            }
            else
            {
                GetTarget();
            }
        }

        private void TryAttack()
        {
            if(!canAttack)
                return;
            if(attackTimer < attackRate)
                return;
            attackTimer = 0;
            Shoot();

        }

        private void Shoot()
        {
            Bullet instantiatedBullet = Instantiate(bullet);
            instantiatedBullet.transform.position = transform.position;

            instantiatedBullet.Target = target;
        }

        private GameObject GetTarget()
        {
            if (!Physics2D.CircleCast(position, attackRadius, Vector2.zero, 0, LayerMask.GetMask("Unit")))
                return null;

            target = Physics2D.OverlapCircle(position, attackRadius, LayerMask.GetMask("Unit")).gameObject;
            
            return target;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, attackRadius);
        }
    }
}