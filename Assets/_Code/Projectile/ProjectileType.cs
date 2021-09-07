using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace _Code
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileType : MonoBehaviour
    {
        [SerializeField] private ProjectileDeath _death;
        [SerializeField] private string _enemyTag;
        private Vector3 shotDirection;
        private float projectileSpeed;
        
        public void Setup(Vector3 direction, float speed)
        {
            shotDirection = direction;
            projectileSpeed = speed;
            _death.StoreDeathDirection(direction);
            
            float angle = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
        }
        
        private void Update()
        {
            this.transform.position += Time.deltaTime*projectileSpeed * shotDirection;
        }

        private void PreDestroy()
        {
            _death.EnableParticleDrift();
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_enemyTag))
            {
                PreDestroy();
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }

    }
}