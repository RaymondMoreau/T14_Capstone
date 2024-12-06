using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class representing a Battler, which holds data and logic for an individual character in battle
public class Battler
{
    // The base stats and information for the battler
    public BattlerBase Base { get; set; }

    // The current HP (Health Points) of the battler
    public int HP { get; set; }

    // List of moves that the battler can use in battle
    public List<Move> Moves { get; set; }

    // Constructor to initialize a battler with its base stats and moves
    public Battler(BattlerBase bBase) {
        Base = bBase; // Assign the base stats
        HP = MaxHP; // Set the HP to the maximum at the start

        Moves = new List<Move>(); // Initialize the moves list
        foreach (var move in Base.LearnableMoves) {
            // Add each learnable move to the battler's moves
            Moves.Add(new Move(move.Base));
        }
    }

    // Property to get the maximum HP of the battler based on its base stats
    public int MaxHP {
        get { return Base.maxHP; }
    }

    // Method to simulate the battler taking damage
    // Returns true if the battler's HP reaches 0, indicating they are "defeated"
    public bool TakeDamage() {
        int damage = Random.Range(5, 15); // Generate random damage between 5 and 15
        HP -= damage; // Reduce the battler's HP by the damage amount

        // Ensure HP doesn't drop below 0
        if (HP <= 0) {
            HP = 0; // Cap HP at 0
            return true; // Battler is defeated
        }

        return false; // Battler is still alive
    }
}