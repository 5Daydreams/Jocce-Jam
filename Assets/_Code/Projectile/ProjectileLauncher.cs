using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace _Code
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private ProjectileType _projectilePrefab;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float firingRate;
        private Vector3 firingDirection;
        private bool triggerHeld;
        private bool isShooting;
        private float shootTime= 0;

        public void SetShootDirection(Vector3 direction)
        {
            firingDirection = direction;
        }

        public void SetTriggerHeld(bool value)
        {
            triggerHeld = value;
        }

        private void Update()
        {
            if (!triggerHeld)
            {
                isShooting = false;
                return;
            }

            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(ShootCo(firingDirection));
                shootTime = 0;
            }

            shootTime += Time.deltaTime;

            if (shootTime < 1.0f / firingRate)
            {
                return;
            }

            StartCoroutine(ShootCo(firingDirection));
            shootTime = 0;
        }

        private IEnumerator ShootCo(Vector3 direction)
        {
            ProjectileType proj = Instantiate(_projectilePrefab, this.transform.position, quaternion.identity);
            proj.Setup(direction, _projectileSpeed);
            yield return new WaitForSeconds(firingRate);
        }
    }
}