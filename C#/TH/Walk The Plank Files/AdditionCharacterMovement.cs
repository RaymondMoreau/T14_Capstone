using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AdditionCharacterActions : MonoBehaviour //script that controls the characters movement for addition game
{
    public Rigidbody2D character; 
    public AdditionLogicScript logic; //reference the the addition logic script
    public bool gameNotOver = true;
    public int lives = 3;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Add").GetComponent<AdditionLogicScript>(); // finds the logic script based on assigned tag "Add"
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0) //verifies if the user is out of lives. If yes the game is over 
        {
            logic.gameOver();
            gameNotOver = false;
        }
        else if (logic.score == 6) //verifies if the user has reached the score goal. If yes game is won
        {
            logic.gameWon();
            gameNotOver = false;
        }
    }


    public void jump() //function that moves the character forward when they get a correct answer and the game isn't over
    {
        if(gameNotOver == true)
        {
            float xCord = transform.position.x + 1;
            float yCord = transform.position.y;
            Vector3 pos = new Vector3(xCord, yCord);
            character.MovePosition(pos);
        }
            
    }

    public int lifeLost() //function that removes a life from user if they get an answer wrong
    {
        lives = lives - 1;
        return lives;
    }
}
