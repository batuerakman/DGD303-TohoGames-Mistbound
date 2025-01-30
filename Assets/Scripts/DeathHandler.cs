using UnityEngine;
using Shmup;
using System.Collections;

public class DeathHandler : MonoBehaviour
{
    private void Start()
    {
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.onDeath.AddListener(OnPlayerDeath);
        }
    }

    private void OnPlayerDeath()
    {
        // Get current score and save if it's a high score
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            GameManager.Instance.SaveHighScore(scoreManager.GetCurrentScore());
        }
        
        // Return to main menu
        GameManager.Instance.LoadMainMenu();
    }
} 