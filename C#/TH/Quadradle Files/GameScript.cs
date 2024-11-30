using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public void playButton() //button to start the quadradle game
    {
        SceneManager.LoadScene("QuadradleScene");
    }

    public void menuButton() //button that takes you to the menu
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
