using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (highScoreText != null)
        {
            float highScore = GameManager.Instance.GetHighScore();
            highScoreText.text = Mathf.FloorToInt(highScore).ToString("D7");
        }
    }

    public void OnStartClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void OnCreditsClicked()
    {
        GameManager.Instance.LoadCredits();
    }

    public void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnBackToMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
} 