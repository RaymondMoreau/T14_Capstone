using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public void additionStartButton() //Function that loads the addition part of the game by loading the addition scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        
        
    }

    public void multiplicationStartButton() //Function that loads the multiplication part of the game by loading the multiplication scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
