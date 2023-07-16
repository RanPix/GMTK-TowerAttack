using System;
using UnityEngine;

namespace Towers
{
    public class Tower: MonoBehaviour
    {
        [field: SerializeField] public int TowerTier { get; private set; }
        [Space]
        [SerializeField] protected Bullet bullet;
        [SerializeField] protected Transform bulletPos;
        [Space]
        [SerializeField] protected float attackRadius;
        [SerializeField] protected float attackRate;
        [Space]
        [SerializeField] protected Transform rotatablePart;
        [SerializeField] protected GameObject target;
        [Space]
        [SerializeField] protected LayerMask unitLM;
        [Space]
        [SerializeField] protected AudioType audioType;

        protected bool canAttack;
        protected float attackTimer;

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
            => canAttack = true;

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
            var validationKey = new AudioValidationKey(AudioKind.Towers, audioType, "Shoot");

            AudioSystem.Instance.PlaySound(validationKey, AudioKind.Towers);

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