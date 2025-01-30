using UnityEngine;
using TMPro;
using System.Collections;
using Shmup;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI healthText;
    
    private int currentWave = 0;
    private int maxWaves = 5;
    private int baseEnemyCount = 3;
    private int enemiesRemaining;
    private Health playerHealth;
    private Color originalColor;
    private Vector3 originalHealthPosition;
    private bool isShaking = false;
    private float shakeDuration = 0.2f;
    private float shakeAmount = 5f;
    private float colorFadeDuration = 0.5f;
    private bool gameWon = false;
    private int winEnemyCount = 16;

    private void Start()
    {
        StartCoroutine(InitializeWaveManager());
    }

    private IEnumerator InitializeWaveManager()
    {
        // Wait for at least one frame to ensure all objects are initialized
        yield return new WaitForEndOfFrame();
        
        // Find player and get health component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
            playerHealth.onHealthChanged.AddListener(OnHealthChanged);
        }
        
        if (healthText != null)
        {
            originalColor = healthText.color;
            originalHealthPosition = healthText.transform.localPosition;
        }
        
        StartNextWave();
    }

    private void OnHealthChanged(float health)
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {Mathf.CeilToInt(health)}";
            
            // Only trigger effects if health decreased
            if (health < float.Parse(healthText.text.Split(':')[1]))
            {
                StartCoroutine(ShakeText());
                StartCoroutine(FlashTextColor());
            }
        }
    }

    private IEnumerator ShakeText()
    {
        if (!isShaking)
        {
            isShaking = true;
            float elapsed = 0f;
            
            while (elapsed < shakeDuration)
            {
                elapsed += Time.deltaTime;
                float percentComplete = elapsed / shakeDuration;
                
                float damping = 1.0f - percentComplete;
                float offsetX = Random.Range(-1f, 1f) * shakeAmount * damping;
                float offsetY = Random.Range(-1f, 1f) * shakeAmount * damping;
                
                healthText.transform.localPosition = originalHealthPosition + new Vector3(offsetX, offsetY, 0);
                
                yield return null;
            }
            
            healthText.transform.localPosition = originalHealthPosition;
            isShaking = false;
        }
    }

    private IEnumerator FlashTextColor()
    {
        Color damageColor = Color.red;
        healthText.color = damageColor;
        
        float elapsed = 0f;
        while (elapsed < colorFadeDuration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / colorFadeDuration;
            
            healthText.color = Color.Lerp(damageColor, originalColor, percentComplete);
            
            yield return null;
        }
        
        healthText.color = originalColor;
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        
        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaveCompleteSequence());
        }
    }

    private IEnumerator WaveCompleteSequence()
    {
        if (waveText != null)
        {
            waveText.enabled = true;
            waveText.text = "WAVE CLEARED";
            
            yield return new WaitForSeconds(2f);
            
            waveText.enabled = false;
            
            yield return new WaitForSeconds(1f);
        }
        
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        
        if (currentWave <= maxWaves)
        {
            int enemiesInWave = baseEnemyCount + ((currentWave - 1) * 2);
            enemiesRemaining = enemiesInWave;
            
            // Check win condition
            if (enemiesInWave > winEnemyCount && !gameWon)
            {
                gameWon = true;
                StartCoroutine(WinSequence());
                return;
            }
            
            // Update enemy spawner max enemies
            enemySpawner.SetMaxEnemies(enemiesInWave);
            
            // Show wave start text
            if (waveText != null)
            {
                waveText.enabled = true;
                waveText.text = $"WAVE {currentWave}";
                StartCoroutine(HideWaveText());
            }
        }
    }

    private IEnumerator HideWaveText()
    {
        yield return new WaitForSeconds(2f);
        if (waveText != null)
        {
            // Only disable the text component, not the entire GameObject
            waveText.enabled = false;
        }
    }

    private IEnumerator WinSequence()
    {
        if (waveText != null)
        {
            waveText.enabled = true;
            waveText.text = "YOU WIN";
            
            yield return new WaitForSeconds(3f);
            
            waveText.enabled = false;
        }
        
        // Return to main menu
        GameManager.Instance.LoadMainMenu();
    }
} 