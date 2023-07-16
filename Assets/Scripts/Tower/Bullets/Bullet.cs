using UnityEngine;

namespace Towers
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Damage Damage;
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected LayerMask unitLayer;

        protected GameObject target;

        private void Update()
        {
            if (!target)
            {
                Destroy(gameObject);
                return;
            }
            transform.LookAt(target.transform);
            
            Vector3 direction = (target.transform.position - transform.position).normalized;
            
            transform.position += direction * movementSpeed * Time.deltaTime;
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == target)
            {
                target.GetComponent<UnitBase>().HP.DealDamage(Damage);
                Destroy(gameObject);
            }
        }

        public void SetTarget(GameObject target)
            => this.target = target;
    }
}