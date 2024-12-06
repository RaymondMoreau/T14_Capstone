using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Main class for managing the battle system
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit; // Player's battle unit
    [SerializeField] BattleHud playerHud;   // HUD for the player's unit

    [SerializeField] BattleUnit enemyUnit;  // Enemy's battle unit
    [SerializeField] BattleHud enemyHud;    // HUD for the enemy's unit
    [SerializeField] public BattleDialogBox dialogBox; // Dialog box for battle messages and questions

    public BattleState state;   // Current state of the battle
    public int currentMove;     // Tracks the player's current move selection

    // Called when the script is first loaded
    private void Start()
    {
        StartCoroutine(SetupBattle()); // Initialize the battle
    }

    // Coroutine to set up the battle environment
    public IEnumerator SetupBattle()
    {
        // Initialize player and enemy units and their HUDs
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Battler);

        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Battler);

        // Fetch a question from the database and display it
        StartCoroutine(dialogBox.SetQandAFromDatabase());

        // Display an introductory dialog
        yield return dialogBox.TypeDialog("Correctly answer the question to attack! Press Z to select the correct answer to the question");
        yield return new WaitForSeconds(1f);

        PlayerAction(); // Transition to PlayerAction state
    }

    // Enables the player to select an action
    void PlayerAction()
    {
        state = BattleState.PlayerAction; // Set the state to PlayerAction
        dialogBox.EnableActionSelector(true); // Show action selector UI
    }

    // Enables the player to select a move (answer a question)
    void PlayerMove()
    {
        state = BattleState.PlayerMove; // Set the state to PlayerMove
        dialogBox.EnableActionSelector(false); // Hide action selector
        dialogBox.EnableDialogText(false); // Hide dialog text
        dialogBox.EnableMoveSelector(true); // Show move selector UI
    }

    // Coroutine to handle the player's selected move
    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy; // Set the state to Busy during move execution

        if (currentMove == dialogBox.correctAnswerIndex) // Check if the player's answer is correct
        {
            yield return dialogBox.TypeDialog("Your answer is correct! Attacking the enemy!");

            yield return new WaitForSeconds(1f);

            // Deal damage to the enemy
            bool isFainted = enemyUnit.Battler.TakeDamage();
            yield return enemyHud.UpdateHP(); // Update the enemy's HP bar

            // Fetch a new question
            StartCoroutine(dialogBox.SetQandAFromDatabase());

            if (isFainted) // Check if the enemy is defeated
            {
                yield return dialogBox.TypeDialog("You win");
                yield return new WaitForSeconds(1f);
                Start(); // Restart the battle
            }
            else
            {
                PlayerAction(); // Return to player action state
                yield return dialogBox.TypeDialog("Press Z to answer another question");
            }
        }
        else // Handle incorrect answers
        {
            yield return dialogBox.TypeDialog("Your answer was incorrect, you miss your attack!");
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyMove()); // Proceed to enemy's move
        }
    }

    // Coroutine to handle the enemy's move
    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove; // Set the state to EnemyMove
        yield return dialogBox.TypeDialog("The enemy attacks!");

        yield return new WaitForSeconds(1f);

        // Deal damage to the player
        bool isFainted = playerUnit.Battler.TakeDamage();
        yield return playerHud.UpdateHP(); // Update the player's HP bar

        if (isFainted) // Check if the player is defeated
        {
            yield return dialogBox.TypeDialog("You Lose");
            yield return new WaitForSeconds(1f);
            Start(); // Restart the battle
        }
        else
        {
            PlayerAction(); // Return to player action state
            yield return dialogBox.TypeDialog("Press Z to answer another question");
        }
    }

    // Called every frame to handle player input based on the current state
    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection(); // Handle input for action selection
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection(); // Handle input for move selection
        }
    }

    // Handles input for selecting an action (e.g., attack)
    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Player presses Z to attack
        {
            PlayerMove(); // Transition to move selection
        }
    }

    // Handles input for selecting a move (answering a question)
    void HandleMoveSelection()
    {
        // Navigate through move options using arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentMove < 3)
        {
            ++currentMove; // Move to the next option
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentMove > 0)
        {
            --currentMove; // Move to the previous option
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentMove < 2)
        {
            currentMove += 2; // Move two options down
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && currentMove > 1)
        {
            currentMove -= 2; // Move two options up
        }

        dialogBox.UpdateMoveSelection(currentMove); // Update the move selector UI

        if (Input.GetKeyDown(KeyCode.Z)) // Player presses Z to confirm their answer
        {
            dialogBox.EnableMoveSelector(false); // Hide move selector
            dialogBox.EnableDialogText(true); // Show dialog text
            StartCoroutine(PerformPlayerMove()); // Execute the player's move
        }
    }
}