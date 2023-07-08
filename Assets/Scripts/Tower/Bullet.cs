using System;
using UnityEngine;

namespace Towers
{
    public class Bullet : MonoBehaviour
    {
        public GameObject Target;
        [SerializeField] private Damage Damage;
        [SerializeField] private float movementSpeed;
        [SerializeField] private Transform image;

        private void Update()
        {
            if (!Target)
            {
                Destroy(gameObject);
                return;
            }
            image.LookAt(Target.transform);
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            
            transform.position += direction * movementSpeed * Time.deltaTime;
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == Target)
            {
                Target.GetComponent<UnitBase>().HP.DealDamage(Damage);
                print(Target.GetComponent<UnitBase>().HP.Current);
            }
        }

    }
}