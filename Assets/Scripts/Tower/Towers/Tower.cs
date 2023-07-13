using System;
using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(AudioSource))]
    public class Tower: MonoBehaviour
    {
        [field: SerializeField] public int TowerTier { get; private set; }

        [SerializeField] protected Bullet bullet;
        [SerializeField] protected Transform bulletPos;

        [SerializeField] protected float attackRadius;
        [SerializeField] protected float attackRate;

        [SerializeField] protected Transform rotatablePart;
        [SerializeField] protected GameObject target;

        [SerializeField] protected LayerMask unitLM;

        protected bool canAttack;
        protected float attackTimer;

        private AudioSource shootSource;


        [HideInInspector] public Vector2 Position
        {
            get => transform.position;
            protected set => transform.position = value;
        }

        public Action OnReload;

        private void Start()
        {
            SetUpTower();
        }

        protected void SetUpTower()
        {
            shootSource = GetComponent<AudioSource>();

            canAttack = true;
        }

        private void Update()
        {
            if(!target)
                FindTarget();

            Reload();

            LocateTarget();
        }

        private void Reload()
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackRate)
                OnReload?.Invoke();
        }

        private void LocateTarget()
        {
            if (TargetIsInRadius())
            {
                rotatablePart.LookAt(target.transform);
                TryAttack();

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
            => ((Vector2)target.transform.position - Position).sqrMagnitude <= attackRadius * attackRadius;

        private void TryAttack()
        {
            if(!canAttack)
                return;

            if(attackTimer < attackRate)
                return;

            attackTimer = 0;

            Shoot();
        }

        protected virtual void Shoot()
        {
            shootSource.Play();

            Bullet instantiatedBullet = Instantiate(bullet);
            instantiatedBullet.transform.position = bulletPos.position;

            instantiatedBullet.SetTarget(target);
        }

        private void FindTarget()
        {
            if (!Physics2D.CircleCast(Position, attackRadius, Vector2.zero, 0, unitLM))
                return;

            target = Physics2D.OverlapCircle(Position, attackRadius, unitLM).gameObject;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, attackRadius);
        }
    }
}