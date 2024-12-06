using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class representing a battle unit (player or enemy) in the game
public class BattleUnit : MonoBehaviour
{
    [SerializeField] BattlerBase _base; // Reference to the base stats and data of the unit
    [SerializeField] bool isPlayerUnit; // Flag to indicate if this unit belongs to the player

    public Battler Battler { get; set; } // The active battler object associated with this unit

    // Sets up the battle unit by initializing its battler with base stats
    public void Setup()
    {
        Battler = new Battler(_base); // Create a new Battler instance using the base stats
    }

    // Placeholder for attack animation (currently commented out)
    /*
    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence(); // Create a new sequence for the animation

        if (isPlayerUnit) // Check if this unit is the player's
        {
            // Move the player's unit slightly forward for the attack animation
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        }
        else
        {
            // Move the enemy's unit slightly forward for the attack animation
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        }

        // Move the unit back to its original position after the attack animation
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }
    */
}