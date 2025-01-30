using UnityEngine;

namespace Shmup {
    public class ProjectileController : MonoBehaviour {
        [SerializeField] private float damage = 10f;
        [SerializeField] private float speed = 6f;
        [SerializeField] private float lifetime = 3f;
        
        public LayerMask targetLayers;
        private Rigidbody2D rb;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, lifetime);
        }

        private void FixedUpdate() {
            rb.velocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            // Check if the collided object's layer is in our target layers
            if ((targetLayers.value & (1 << other.gameObject.layer)) != 0) {
                // Get Health component and apply damage
                Health health = other.gameObject.GetComponent<Health>();
                if (health != null) {
                    health.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
} 