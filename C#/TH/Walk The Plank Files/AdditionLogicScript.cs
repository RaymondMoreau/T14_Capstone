using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class AdditionLogicScript : MonoBehaviour //script for the addition functionalities of the Walk the Plank game
{
    public AdditionCharacterActions character;
    void Start() //function that runs at the start of the scene/game
    {
        randomizeQuestionAddition(); //1st question gets randomized at the start
        answerOptionsAddition(); //1st answer options gets randomized at the start
    }

    public TextMeshProUGUI questionText; //public variable to change the text of the question 
    private int number1, number2; 
    [ContextMenu("Randomize Question")]
    public void randomizeQuestionAddition() //function that randomizes the addition questions
    {
        number1 = Random.Range(2, 13);
        number2 = Random.Range(2, 13);
        questionText.text = number1.ToString() + " + " + number2.ToString();
        answerOptionsAddition();
    }

    private int option1, option2, correct;
    private int[] options; 
    public TextMeshProUGUI textOption1, textOption2, textOption3; //answer options text 
    [ContextMenu("Randomize Options")]
    public void answerOptionsAddition() // function that randomizes answer options based on numbers randomly generated previously
    {
        correct = number1 + number2;
        option1 = (number1 - 1) + number2;
        option2 = (number1 + 1) + number2;
        options = new int[] { correct, option1, option2 };

        randomizeArrayValues(options);

        textOption1.text = options[0].ToString();
        textOption2.text = options[1].ToString();
        textOption3.text = options[2].ToString();

    }
    public void randomizeArrayValues(int[] values) //function that randomizes order of an array to ensure that the correct answer isn't always in the same place
    {
        for (int posOfArray = 0; posOfArray < values.Length; posOfArray++)
        {
            int temp = values[posOfArray];
            int randomizeArray = Random.Range(0, posOfArray + 1);
            values[posOfArray] = values[randomizeArray];
            values[randomizeArray] = temp;
        }
    }

    public int scoreGoal, score; //number of questions user has to answer correctly to win the game
    public void checkAnswer1() //checks answer for the first option/button
    {
        if (textOption1.text.ToString() == correct.ToString())
        {
            score += 1;
           character.jump();
        }
        else
        {
            character.lifeLost();
        }
    }
    public void checkAnswer2() //checks answer for the second option/button
    {
        if (textOption2.text.ToString() == correct.ToString())
        {
            score += 1;
            character.jump();

        }
        else
        {
            character.lifeLost();
        }
    }
    public void checkAnswer3() //checks answer for the third option/button
    {
        if (textOption3.text.ToString() == correct.ToString())
        {
            score += 1;
            character.jump();
        }
        else
        {
            character.lifeLost();
        }
    }

    public GameObject gameOverScreen; //Screen assigned when game is lost
    public GameObject gameWonScreen; //screen assigned when game is won
    public GameObject questionOptions; //the answer options
    public void restartGame() //function that starts the current game from the beginning
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu() //function that brings you back to the main menu
    {
        SceneManager.LoadScene("Menu");
    }

    public void gameOver() //funciton that activates when game is lost to activate gameOverScreen and to remove the asnwer options to prevent user from clicking them anymore
    {
        gameOverScreen.SetActive(true);
        questionOptions.SetActive(false);
    }

    public void gameWon() //function that activates when game is won to activate gameWonScreen and to remove the asnwer options to prevent user from clicking them anymore
    {
        gameWonScreen.SetActive(true);
        questionOptions.SetActive(false);
    }

}