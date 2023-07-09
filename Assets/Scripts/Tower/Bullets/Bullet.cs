using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Towers
{
    public class Bullet : MonoBehaviour
    {
        public GameObject Target;
        [SerializeField] protected Damage Damage;
        [SerializeField] protected float movementSpeed;
        //[FormerlySerializedAs("image")] [SerializeField] protected Transform imageHolder;
        

        private void Update()
        {
            if (!Target)
            {
                Destroy(gameObject);
                return;
            }
            transform.LookAt(Target.transform);
            
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            
            transform.position += direction * movementSpeed * Time.deltaTime;
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == Target)
            {
                Target.GetComponent<UnitBase>().HP.DealDamage(Damage);
                Destroy(gameObject);
            }
        }

    }
}