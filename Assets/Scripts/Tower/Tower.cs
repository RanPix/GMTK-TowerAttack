using UnityEngine;

namespace Towers
{
    public class Tower: MonoBehaviour
    {
        [field: SerializeField] public int TowerTier { get; private set; }

        [SerializeField] protected Bullet bullet;

        [SerializeField] protected float attackRadius;
        [SerializeField] protected float attackRate;
        protected float attackTimer;

        public Action OnReload;

        [SerializeField] protected Transform rotatablePart;

        [SerializeField] protected GameObject target;
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
            
            if(attackTimer >= attackRate)
                OnReload?.Invoke();
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

        protected virtual void Shoot()
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