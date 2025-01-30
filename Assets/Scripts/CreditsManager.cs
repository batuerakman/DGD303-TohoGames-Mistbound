using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public void OnBackToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMainMenu();
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
        }
    }
} 