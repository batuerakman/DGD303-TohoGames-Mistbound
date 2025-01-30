using UnityEngine;
using UnityEngine.Events;

namespace Shmup {
    public class Health : MonoBehaviour {
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;
        private ScoreManager scoreManager;
        private WaveManager waveManager;

        public UnityEvent onDeath;
        public UnityEvent<float> onHealthChanged;

        private HitFlash hitFlash;

        private void Awake()
        {
            hitFlash = GetComponent<HitFlash>();
        }

        
        private void Start() {
            currentHealth = maxHealth;
            onHealthChanged?.Invoke(currentHealth);
            // Find the ScoreManager in the scene
            scoreManager = FindObjectOfType<ScoreManager>();
            waveManager = FindObjectOfType<WaveManager>();
        }

        public void TakeDamage(float damage) {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            onHealthChanged?.Invoke(currentHealth);
            if (hitFlash != null)
            {
                hitFlash.Flash();
            }
            
            if (currentHealth <= 0) {
                // Add points when enemy dies
                if (scoreManager != null && CompareTag("Enemy"))
                {
                    scoreManager.AddKillPoints();
                    if (waveManager != null)
                    {
                        waveManager.EnemyDefeated();
                    }
                }
                onDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
} 