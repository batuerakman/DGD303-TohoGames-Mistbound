using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
namespace Shmup {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] List<EnemyType> enemyTypes;
        [SerializeField] int maxEnemies = 4;
        [SerializeField] float spawnInterval = 2f;
        [SerializeField] float intervalVariance = 0.5f;
        List<SplineContainer> splines;
        EnemyFactory enemyFactory;
        
        float spawnTimer;
        int enemiesSpawned;
        void OnValidate() {
            splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        }
        void Start() {
            enemyFactory = new EnemyFactory();
            splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
            
            // Validate required components
            if (enemyTypes == null || enemyTypes.Count == 0) {
                Debug.LogError("No enemy types assigned to EnemySpawner!");
                enabled = false;
                return;
            }
            
            if (splines == null || splines.Count == 0) {
                Debug.LogError("No splines found in children of EnemySpawner!");
                enabled = false;
                return;
            }
        }
        void Update() {
            spawnTimer += Time.deltaTime;
            
            if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval) {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
        public void SetMaxEnemies(int count)
{
    maxEnemies = count;
    enemiesSpawned = 0;
    spawnTimer = spawnInterval; // Reset timer to spawn immediately
}
        void SpawnEnemy() {
            if (enemyTypes.Count == 0 || splines.Count == 0) return;
            
            EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];
            SplineContainer spline = splines[Random.Range(0, splines.Count)];
            
            // TODO: Possible optimization - pool enemies
            enemyFactory.CreateEnemy(enemyType, spline);
            enemiesSpawned++;
            
            // Set next spawn time with randomization
            spawnTimer = -Random.Range(-intervalVariance, intervalVariance);
        }
    }
}