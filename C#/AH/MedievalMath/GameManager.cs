using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject popupPanel; // Reference to the popup panel
    public TMP_Text resultText;  // Reference to the result text
    public Button restartButton; // Reference to the restart button

    private bool gameEnded = false;

    void Start()
    {
        // Ensure the popup panel is initially hidden
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }

        // Add a listener to the restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void EndGame(bool isVictory)
    {
        if (gameEnded) return; // Prevent multiple triggers
        gameEnded = true;

        // Show the popup panel
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }

        // Set the result text
        if (resultText != null)
        {
            resultText.text = isVictory ? "Victory!" : "Defeat!";
            resultText.color = isVictory ? Color.green : Color.red;
        }

        // Pause the game
        Time.timeScale = 0f; // Freeze the game
    }

    public void RestartGame()
    {
        // Restart the game by reloading the current scene
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}