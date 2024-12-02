using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] BattlerBase _base;
    [SerializeField] bool isPlayerUnit;
    public Battler Battler {get; set;}



    public void Setup(){
        Battler = new Battler(_base);

    }


    //public void PlayAttackAnimation(){
     //   var sequence = DOTween.Sequence();
        
     //   if(isPlayerUnit){
     //       sequence.Append(image.transform.DOLocalMoveX(originalPos.x+ 50f, 0.25f));
      //  }

    //    else{
     //       sequence.Append(image.transform.DOLocalMoveX(originalPos.x +50f,0.25f));
    //   }

    //    sequence.Append(image.transform.DOLocalMoveX(originalPos.x,0.25));
 // }

}
