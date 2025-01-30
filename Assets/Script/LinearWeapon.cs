using UnityEngine;

namespace Shmup {
    public class LinearWeapon : MonoBehaviour {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float fireRate = 0.2f;
        
        private float fireTimer;
        
        private void Start()
        {
            // Set the layer for this weapon's projectiles
            projectilePrefab.layer = LayerMask.NameToLayer("PlayerProjectile");
        }
        
        private void Update()
        {
            fireTimer += Time.deltaTime;
            
            // Only fire if space is held and enough time has passed
            if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
            {
                FireProjectile();
                fireTimer = 0f;
            }
        }
                
        private void FireProjectile() {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.layer = LayerMask.NameToLayer("PlayerProjectile");
            
            var projectileController = projectile.GetComponent<ProjectileController>();
            if (projectileController != null)
            {
                projectileController.targetLayers = 1 << LayerMask.NameToLayer("Enemy");
            }
        }
    }
} 