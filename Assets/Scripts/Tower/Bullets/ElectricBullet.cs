using UnityEngine;

namespace Towers
{
    public class ElectricBullet : Bullet
    {
        [SerializeField] private SpriteRenderer bulletImage;
        [SerializeField] private GameObject bulletPrefab;
        
        [SerializeField] private Transform nextBulletPos;

        [SerializeField] private float attackRadius;


        private ElectricBullet nextChild;
        private ElectricBullet previousChild;
        private bool childWasSpawned;

        private float lastSpawn = 0;
        private float timer = 0;

        private void Start()
        {
            transform.LookAt(target.transform);
            transform.rotation *= Quaternion.Euler(90,0,0);
            childWasSpawned = false;
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (!target)
                Destroy(gameObject);

            if(timer < lastSpawn  + 0.1f)
                return;

            if (childWasSpawned)
                return;

            SpawnChild();
        }

        private void SpawnChild()
        {
            nextChild = Instantiate(bulletPrefab).GetComponent<ElectricBullet>();
            nextChild.transform.position = nextBulletPos.position;
            nextChild.SetTarget(target);
            nextChild.previousChild = this;
            lastSpawn = Time.deltaTime;

            childWasSpawned = true;
        }

        private void OnDestroy()
        {
            if (previousChild)
            {
                previousChild.target = null;
                Destroy(previousChild.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(nextBulletPos.position, attackRadius);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject != target)
                return;

            target.GetComponent<UnitBase>().HP.DealDamage(Damage);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(nextBulletPos.position, attackRadius, Vector2.zero, 0, unitLayer);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject != target)
                {
                    SetTarget(hit.collider.gameObject);
                    return;
                }

            }

            target = null;

            Destroy(gameObject);
        }
    }
}