using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject helpMenu;

    public void loadGame() //loads main game scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void helpMenuOpen() //opens the help menu
    {
        helpMenu.SetActive(true);
    }

    public void helpMenuClose() //closes the help menu
    {
        helpMenu.SetActive(false);
    }
}
