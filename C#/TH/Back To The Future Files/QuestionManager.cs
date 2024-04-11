using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour //script to manage the question and answer
{
    private int correctAnswer = 3058; //hardcoded correct answer for demo purposes only, answer will be generated randomly in future
    private int attempts = 2;
    private int lives = 3;
    public TextMeshProUGUI answer1, answer2, answer3;
    public GameObject gameWonScreen; //game won screen
    public GameObject gameText; //question text
    public GameObject gameAnswerOptions; //question options
    public GameObject gameOverScene; //game over screen
   
    // Update is called once per frame
    void Update()
    {
        if (attempts == 0) //checks if user has any attempts left
        {
            /* Put up display helper guy (not implemented yet) */
        }
        if (lives == 0) //checks if user has any lives yet
        {
            gameOverScene.SetActive(true); //activates game over screen
            gameText.SetActive(false); //removes question text

        }
    }
    
    public void checkAnswer1() //checks first answer button
    {
        if (answer1.text.ToString() == correctAnswer.ToString()) 
        {
            gameWonScreen.SetActive(true);
            gameText.SetActive(false);
        }
        else
        {
            attempts -= 1;
            lives -= 1;
        }
    }

    public void checkAnswer2() //checks second answer button
    {
        if (answer2.text.ToString() == correctAnswer.ToString())
        {
            gameWonScreen.SetActive(true);
            gameText.SetActive(false);
        }
        else
        {
            attempts -= 1;
            lives -= 1;
        }
    }

    public void checkAnswer3() //checks third answer button
    {
        if (answer3.text.ToString() == correctAnswer.ToString())
        {
            gameWonScreen.SetActive(true);
            gameText.SetActive(false);
        }
        else
        {
            attempts -= 1;
            lives -= 1;
        }
    }

    public void backToMenu() //loads back to the menu
    {
        SceneManager.LoadScene("Menu");
    }

    public void restartGame() //restarts current game
    {
        SceneManager.LoadScene("QuestionScene");
    }

}
