using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler
{
    public BattlerBase Base {get; set;}

    public int HP {get; set;}
    public List<Move> Moves {get; set;}

    public Battler(BattlerBase bBase){
        Base=bBase;
        HP = MaxHP;

        Moves = new List<Move>();
         foreach (var move in Base.LearnableMoves){
            Moves.Add(new Move(move.Base));
         }
    }
    public int MaxHP {
        get{ return Base.maxHP;}
    }

    public bool TakeDamage() {
        int damage = Random.Range(5,15);
        HP-=damage;

        if(HP <= 0 ){
            HP=0;
            return true;
        }
        return false;


    }

}
