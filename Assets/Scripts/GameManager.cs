using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private const string HIGH_SCORE_KEY = "HighScore";
    private GameObject mainMenuCanvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Find and store reference to main menu canvas
        mainMenuCanvas = GameObject.Find("Canvas");
        if (mainMenuCanvas != null)
        {
            DontDestroyOnLoad(mainMenuCanvas);
        }
    }

    public void SaveHighScore(float score)
    {
        float currentHighScore = PlayerPrefs.GetFloat(HIGH_SCORE_KEY, 0f);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetFloat(HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();
        }
    }

    public float GetHighScore()
    {
        return PlayerPrefs.GetFloat(HIGH_SCORE_KEY, 0f);
    }

    public void StartGame()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
        SceneManager.LoadScene("mainmenu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        // Get current scene index and reload it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}