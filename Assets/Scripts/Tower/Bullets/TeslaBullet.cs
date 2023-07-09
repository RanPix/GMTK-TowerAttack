using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Towers
{
    public class TeslaBullet : Bullet
    {
        [SerializeField] private SpriteRenderer bulletImage;
        [SerializeField] private GameObject bulletPrefab;
        
        [SerializeField] private Sprite[] randomSprites;
        [SerializeField] private Sprite[] hitParticles;

        [SerializeField] private Transform nextBulletPos;

        [SerializeField] private float attackRadius;

        [SerializeField] private Tower tower;

        private TeslaBullet nextChild;
        private TeslaBullet previousChild;
        private bool childWasSpawned;

        private void Start()
        {
            transform.LookAt(Target.transform);
            transform.rotation *= Quaternion.Euler(90,0,0);
            childWasSpawned = false;
        }

        private float lastSpawn = 0;
        private float timer = 0;
        private void Update()
        {
            timer += Time.deltaTime;
            if (!Target)
            {
                
                Destroy(gameObject);
            }
            if(timer < lastSpawn  + 0.4f)
                return;
            if (!childWasSpawned)
            {
                nextChild = Instantiate(bulletPrefab).GetComponent<TeslaBullet>();
                nextChild.transform.position = nextBulletPos.position;
                nextChild.Target = Target;
                nextChild.tower = tower;
                nextChild.previousChild = this;
                lastSpawn = Time.deltaTime;
                
                childWasSpawned = true;
            }
        }

        private void OnDestroy()
        {
            if (previousChild)
            {
                previousChild.Target = null;
                Destroy(previousChild.gameObject);
            }
        }

        private GameObject GetTarget()
        {
            if (!Physics2D.CircleCast(nextBulletPos.position, attackRadius, Vector2.zero, 0, LayerMask.GetMask("Unit")))
                return null;
            if (Target)
                return Physics2D.OverlapCircle(nextBulletPos.position, attackRadius, LayerMask.GetMask("Unit")).gameObject;
            
            Target = Physics2D.OverlapCircle(nextBulletPos.position, attackRadius, LayerMask.GetMask("Unit")).gameObject;
            return Target;
        }

        public void SetRandomSprite()
        {
            bulletImage.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(nextBulletPos.position, attackRadius);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == Target)
            {
                Target.GetComponent<UnitBase>().HP.DealDamage(Damage);
                
               RaycastHit2D[] hits = Physics2D.CircleCastAll(nextBulletPos.position, attackRadius, Vector2.zero, 0,
                    LayerMask.GetMask("Unit"));
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.gameObject != Target)
                    { 
                        print(hit.collider.gameObject + "   --   " + Target);
                        Target = hit.collider.gameObject;
                        return;
                    }
                    
                }

                Target = null;
                Destroy(gameObject);
            }
        }
    }
}