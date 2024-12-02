using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlayPanel; // Reference to the HowToPlayPanel
    public Button closeButton;        // Reference to the CloseButton

    void Start()
    {
        // Ensure the panel is active initially
        howToPlayPanel.SetActive(true);

        // Add listener to close the panel
        closeButton.onClick.AddListener(CloseHowToPlay);

        Time.timeScale = 0f;
    }

    void CloseHowToPlay()
    {
        // Deactivate the HowToPlayPanel
        howToPlayPanel.SetActive(false);

        // Optionally, resume the game if it was paused
        Time.timeScale = 1f;
    }
}