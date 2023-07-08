using System;
using UnityEngine;

namespace Tower
{
    public class Bullet : MonoBehaviour
    {
        public GameObject Target;
        [SerializeField] private Damage Damage;
        [SerializeField] private float movementSpeed;

        private void Update()
        {
            if (!Target)
                Destroy(gameObject);
            transform.LookAt(Target.transform);
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            
            transform.position += direction * movementSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject == Target)
            {
                Health targetHealth = Target.GetComponent<Health>();
                targetHealth.DealDamage(Damage);
            }
        }
    }
}