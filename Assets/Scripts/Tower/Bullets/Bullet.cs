using UnityEngine;

namespace Towers
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Damage Damage;
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected LayerMask unitLayer;
        [SerializeField] protected int pierceAmount;
        [SerializeField] protected float lifeTime = .7f;

        protected GameObject target;
        protected Vector3 direction;
        
        private void Update()
        {
            transform.position += direction * movementSpeed * Time.deltaTime;
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 3)
            {
                other.gameObject.GetComponent<UnitBase>().HP.DealDamage(Damage);
                if(pierceAmount == 0)
                    Destroy(gameObject);
                pierceAmount--;
            }
        }

        public void SetTarget(GameObject target)
        {
            transform.LookAt(target.transform.position);
            direction = (target.transform.position - transform.position).normalized;
            Destroy(gameObject, lifeTime);
        }
            
    }
}