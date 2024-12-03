//imports
using UnityEngine;
using UnityEngine.SceneManagement;

// when player answers the final question correctly, they get brought to this screen, indicating a victory.
public class toVictory : MonoBehaviour
{
    public void toVictoryScene()
    {
        SceneManager.LoadScene("victoryScreen");
    }
}