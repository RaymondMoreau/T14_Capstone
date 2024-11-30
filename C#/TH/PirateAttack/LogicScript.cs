using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject allyShip;
    public EnemyShipSpawner enemySpawner;
    public int lane, answerLane, score, endScore;
    public int lives = 3;
    public TextMeshProUGUI scoreText, livesText, endScoreText;
    public bool firedState, hitState = false;
    public TextMeshProUGUI optionText1, optionText2, optionText3, questionText;

    // Start is called before the first frame update
    void Awake() //loads the correct lane and a random question on start up
    {
        correctLane();
        randomizeQuestion();
    }

    // Update is called once per frame
    void Update() //sets score text and lives text
    {
        scoreText.text = "Score: " + score.ToString();
        endScoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

        //sets current lane position
        if (allyShip.transform.position.y == 0)
        {
            lane = 2;
        }
        else if (allyShip.transform.position.y == 3)
        {
            lane = 1;
        }
        else if (allyShip.transform.position.y == -3)
        {
            lane = 3;
        }
        
        if (lives == 0)
        {
            //game is over
            gameOver();

        }
    }


    public int getAllyShipPosition() //function that gets ally ships position
    {
        int position = (int)allyShip.transform.position.y;
        return position;
    }

    public void spawnShip() //function that spawns enemy ship
    {
        enemySpawner.spawnShips();
    }

    public void increaseSpeed() //function that increases speed of enemy ships
    {
       enemySpawner.enemyShips.GetComponent<EnemyShipMovement>().moveSpeed += 1;
    }

    public float getSpeed() //function that gets speed of enemy ships
    {
        return enemySpawner.enemyShips.GetComponent<EnemyShipMovement>().moveSpeed;
    }

    // Question Randomisation logic
    private int option1, option2, correct;
    private int[] options;
    public int number1, number2;
    public void randomizeQuestion() //function that randomizes the addition questions
    {
        number1 = UnityEngine.Random.Range(2, 13);
        number2 = UnityEngine.Random.Range(2, 13);
        questionText.text = number1.ToString() + " + " + number2.ToString();
        answerOptions();
        Debug.Log(answerLane);
    }

    public void answerOptions() // function that randomizes answer options based on numbers randomly generated previously
    {
        correct = number1 + number2;
        option1 = (number1 - 1) + number2;
        option2 = (number1 + 1) + number2;
        options = new int[] { correct, option1, option2 };

        
        randomizeArrayValues(options);
        answerLane = Array.IndexOf(options, correct) + 1;
        
        optionText1.text = options[0].ToString();
        optionText2.text = options[1].ToString();
        optionText3.text = options[2].ToString();
    }
    public int correctLane()
    {
        return answerLane;
    }
    public void randomizeArrayValues(int[] values) //function that randomizes order of an array to ensure that the correct answer isn't always in the same place
    {
        for (int posOfArray = 0; posOfArray < values.Length; posOfArray++)
        {
            int temp = values[posOfArray];
            int randomizeArray = UnityEngine.Random.Range(0, posOfArray + 1);
            values[posOfArray] = values[randomizeArray];
            values[randomizeArray] = temp;
        }
    }

    public GameObject gameOverPanel;
    public void gameOver() //function that loads game over panel
    {
        gameOverPanel.SetActive(true);
    }

    public void mainMenu() //function that loads main menu
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void restart() //function that re loads game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
