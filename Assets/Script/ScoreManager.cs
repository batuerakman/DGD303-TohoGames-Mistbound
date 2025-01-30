using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float score = 0;
    private float pointsPerSecond = 8f;

    [Header("Shake Settings")]
    [SerializeField] private float maxShakeIntensity = 10f;
    [SerializeField] private float shakeSpeed = 50f;
    [SerializeField] private float maxScoreForShake = 10000f;
    
    private Vector3 originalPosition;
    private float currentShakeIntensity;

    private void Start()
{
    originalPosition = scoreText.transform.localPosition;
    if (scoreText != null)
    {
        // Make sure the score text's parent GameObject is active
        scoreText.transform.parent.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }
    UpdateScoreDisplay();
}
    public float GetCurrentScore()
    {
        return score;
    } 
    private void Update()
    {
        // Add points per second
        score += pointsPerSecond * Time.deltaTime;
        UpdateScoreDisplay();
        
        // Calculate shake intensity based on score
        currentShakeIntensity = (score / maxScoreForShake) * maxShakeIntensity;
        
        // Apply shake effect
        if (score > 0)
        {
            Vector2 shakeOffset = new Vector2(
                Mathf.Sin(Time.time * shakeSpeed) * currentShakeIntensity,
                Mathf.Cos(Time.time * shakeSpeed * 1.2f) * currentShakeIntensity
            );
            
            scoreText.transform.localPosition = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0);
        }

        // Check for ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.LoadMainMenu();
        }
    }

    public void AddKillPoints()
    {
        score += 100f;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        // Format score to 7 digits with leading zeros
        string formattedScore = Mathf.FloorToInt(score).ToString("D7");
        scoreText.text = formattedScore;
    }
}