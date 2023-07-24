using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Towers
{
    public class TowersCreep : Bullet
    {
        [SerializeField] private float findingTargetRadius;
        [SerializeField] private float requiredDistanceSquare;
        
        private Transform currentPatrolPoint;

        private int currentPatrolPointIndex
        {
            get
            {
                if (!currentPatrolPoint)
                    return 0;
                return Array.IndexOf(parent.GetAllPatrolPoints(), currentPatrolPoint);
            }
        }

        private SpawnerTower parent;

        private void Update()
        {
            if (!target)
            {
                FindTarget();
                Patrol();
                return;
            }
            
            Attack();
        }

        private void Patrol()
        {
            if (!currentPatrolPoint)
                currentPatrolPoint = parent.GetNearestPatrolPoint(transform.position);

            if (IsNearPoint())
                currentPatrolPoint = parent.GetNextPatrolPoint(currentPatrolPoint);
            
            
            transform.LookAt(currentPatrolPoint);
            transform.GoTo(currentPatrolPoint.position, movementSpeed);
        }
        private bool IsNearPoint()
        {
            Transform[] patrolPoints = parent.GetAllPatrolPoints();
            
            var distance = patrolPoints[currentPatrolPointIndex].position - transform.position;

            var distanceSqr = distance.sqrMagnitude;

            return distanceSqr <= requiredDistanceSquare;
        }


        private void Attack()
        {
            transform.LookAt(target.transform.position);
            transform.GoTo(target.transform.position, movementSpeed);
        }

        private void FindTarget()
        {
            if (!Physics2D.CircleCast(transform.position, findingTargetRadius, Vector2.zero, 0, LayerMask.GetMask("Unit")))
                return;

            SetTarget(Physics2D.OverlapCircle(transform.position, findingTargetRadius, LayerMask.GetMask("Unit")).gameObject);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, findingTargetRadius);
        }

        public void SetParent(SpawnerTower parent)
        {
            if(this.parent || !parent)
                return;
            this.parent = parent;
        }

        private void OnDestroy()
        {
            parent.OnCreepDestroyed(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.layer != 3)
                return;
            
            other.gameObject.GetComponent<UnitBase>().HP.DealDamage(Damage);
            Destroy(gameObject);
        }
    }
}