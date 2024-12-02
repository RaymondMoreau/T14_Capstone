using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battlers", menuName = "newBattlers/Create new battler")]
public class BattlerBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite Sprite;


    //Base stats
    [SerializeField] public int maxHP;
    [SerializeField] public List<learnableMove> learnableMoves;

    public string Name {
        get {return name;}
    }

    public int MaxHP{
        get {return maxHP;}
    }

    public List<learnableMove> LearnableMoves{
        get {return learnableMoves; }
    }

}
[System.Serializable]
public class learnableMove
{
    [SerializeField] MoveBase moveBase;

    public MoveBase Base{
        get {return moveBase; }
    }
}
