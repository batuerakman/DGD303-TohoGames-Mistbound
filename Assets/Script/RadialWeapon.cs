using UnityEngine;

namespace Shmup {
    public class RadialWeapon : MonoBehaviour {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private LayerMask targetLayer;
        
        private float fireTimer;
        
        private void Start()
        {
            // Set the layer for this weapon's projectiles
            projectilePrefab.layer = LayerMask.NameToLayer("EnemyProjectile");
        }
        
        private void Update() {
            fireTimer += Time.deltaTime;
            
            if (fireTimer >= fireRate) {
                FireProjectiles();
                fireTimer = 0f;
            }
        }
        
        private void FireProjectiles()
        {
            float angleStep = 360f / 6;
            float currentAngle = 0f;
            
            for (int i = 0; i < 6; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.layer = LayerMask.NameToLayer("EnemyProjectile");
                
                var projectileController = projectile.GetComponent<ProjectileController>();
                if (projectileController != null)
                {
                    projectileController.targetLayers = 1 << LayerMask.NameToLayer("Player");
                }
                
                float angleRadians = currentAngle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Sin(angleRadians), Mathf.Cos(angleRadians));
                
                float rotationAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
                
                currentAngle += angleStep;
            }
        }
    }
} 