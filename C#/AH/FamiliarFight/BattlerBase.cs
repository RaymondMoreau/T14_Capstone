using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject class to define a Battler's base stats, sprite, and learnable moves
[CreateAssetMenu(fileName = "Battlers", menuName = "newBattlers/Create new battler")]
public class BattlerBase : ScriptableObject
{
    [SerializeField] string name; // Name of the battler
    [TextArea] [SerializeField] string description; // Description of the battler
    [SerializeField] Sprite Sprite; // Visual representation of the battler

    // Base stats for the battler
    [SerializeField] public int maxHP; // Maximum health points of the battler
    [SerializeField] public List<learnableMove> learnableMoves; // List of moves the battler can learn

    // Property to access the battler's name
    public string Name {
        get { return name; }
    }

    // Property to access the battler's maximum HP
    public int MaxHP {
        get { return maxHP; }
    }

    // Property to access the list of moves the battler can learn
    public List<learnableMove> LearnableMoves {
        get { return learnableMoves; }
    }
}

// Class to define a move that a battler can learn
[System.Serializable]
public class learnableMove
{
    [SerializeField] MoveBase moveBase; // Reference to the base data for the move

    // Property to access the move's base data
    public MoveBase Base {
        get { return moveBase; }
    }
}