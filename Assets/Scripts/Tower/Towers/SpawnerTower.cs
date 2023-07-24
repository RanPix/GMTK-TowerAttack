using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Towers
{
    public class SpawnerTower : Tower
    {
        [SerializeField] private int maxCreepsAmount;
        [Space] 
        [SerializeField] private Transform[] patrolPoints;

        private List<TowersCreep> _spawnedCreeps = new ();

        private GameObject _target;
        protected override GameObject target
        {
            get => _target;
            set
            {
                _target = value;
                
                if(!value)
                    return;
                
                foreach (var creep in _spawnedCreeps)
                {
                    if(!creep)
                        continue;
                    
                    creep.SetTarget(value);
                }

            }
        }
        
        public Action OnReload;

        private void Start()
        {
            SetUpTower();
        }

        private void Update()
        {
            if(!target)
                FindTarget();

            Reload();

            LocateTarget();
            
            if(!canAttack)
                return;

            if(attackTimer < attackRate)
                return;
            
            SpawnCreep();
        }

        public Transform GetNearestPatrolPoint(Vector2 creepPos)
        {
            float minDistance = 0;
            Transform closestPoint = patrolPoints[0];

            for (int i = 0; i < patrolPoints.Length; i++)
            {
                float distance = Vector2.Distance(creepPos, patrolPoints[i].transform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    closestPoint = patrolPoints[i];
                }
            }

            return closestPoint;
        }
        public Transform[] GetAllPatrolPoints()
        {
            return patrolPoints;
        }

        public Transform GetNextPatrolPoint(Transform currentPoint)
            => patrolPoints.GetNextItem(currentPoint);

        private void SetUpTower()
            => canAttack = true;
        
        private void LocateTarget()
        {
            if (TargetIsInRadius())
            {
                rotatablePart.LookAt(target.transform);
                return;
            }
            

            FindTarget();

            rotatablePart.rotation = Quaternion.Euler(rotatablePart.eulerAngles.x, rotatablePart.eulerAngles.y, 0);
        }
        private bool TargetIsInRadius()
        {
            if (!target)                                                
                return false;

            if (!CheckRadius())
            {
                target = null;

                return false;
            }

            return true;
        }
        private bool CheckRadius()
            => ((Vector2)target.transform.position - (Vector2)transform.position).sqrMagnitude <= attackRadius * attackRadius;

        private void Reload()
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackRate)
                OnReload?.Invoke();
        }

        private void FindTarget()
        {
            if (!Physics2D.CircleCast(transform.position, attackRadius, Vector2.zero, 0, unitLM))
                return;

            target = Physics2D.OverlapCircle(transform.position, attackRadius, unitLM).gameObject;
        }



        private void Shoot()
        {
            if(_spawnedCreeps.Count >= maxCreepsAmount)
                return;
            
            PlaySpawnSound();
            SpawnCreep();
        }
        private void PlaySpawnSound()
        {
            var validationKey = new AudioValidationKey(AudioKind.Towers, audioType, "Shoot");

            AudioSystem.Instance.PlaySound(validationKey, AudioKind.Towers);
        }


        private void SpawnCreep()
        {
            if(_spawnedCreeps.Count >= maxCreepsAmount)
                return;
            attackTimer = 0;
            TowersCreep newCreep = Instantiate(bullet) as TowersCreep;
            newCreep.transform.position = GetRandomSpawnPoint() + (Vector2)bulletPos.position;
            newCreep.SetParent(this);
            newCreep.SetTarget(target);
            
            _spawnedCreeps.Add(newCreep);
        }
        
        public void OnCreepDestroyed(TowersCreep destroyedCreep)
        {
            _spawnedCreeps.Remove(destroyedCreep);
        }

        private Vector2 GetRandomSpawnPoint()
            => new (Random.Range(-attackRadius, attackRadius),Random.Range(-attackRadius, attackRadius));

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}