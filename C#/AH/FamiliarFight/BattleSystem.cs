using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;

    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] public BattleDialogBox dialogBox;

    public BattleState state;

    public int currentMove;

    private void Start(){
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Battler);

        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Battler);

        StartCoroutine(dialogBox.SetQandAFromDatabase());

        yield return dialogBox.TypeDialog("Correctly answer the question to attack! Press Z to select the correct answer to the question");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction(){
        state = BattleState.PlayerAction;
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove(){
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove() {
        state = BattleState.Busy;

        if (currentMove == dialogBox.correctAnswerIndex)
        {
            yield return dialogBox.TypeDialog("Your answer is correct! Attacking the enemy!");

            yield return new WaitForSeconds(1f);

            bool isFainted = enemyUnit.Battler.TakeDamage();
            yield return enemyHud.UpdateHP();
            
            StartCoroutine(dialogBox.SetQandAFromDatabase());

            if(isFainted){
                yield return dialogBox.TypeDialog("You win");
                yield return new WaitForSeconds(1f);
                Start();
            }

            else {
                PlayerAction();
                yield return dialogBox.TypeDialog("Press Z to answer another question");
            }



        }

        else {
            yield return dialogBox.TypeDialog("Your answer was incorrect, you miss your attack!");
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyMove());
        }

    }

    IEnumerator EnemyMove(){
        state = BattleState.EnemyMove;
        yield return dialogBox.TypeDialog("The enemy attacks!");

        yield return new WaitForSeconds(1f);
        bool isFainted = playerUnit.Battler.TakeDamage();
        yield return playerHud.UpdateHP();

        if(isFainted){
            yield return dialogBox.TypeDialog("You Lose");
            yield return new WaitForSeconds(1f);
            Start();
        }

        else {
            PlayerAction();
            yield return dialogBox.TypeDialog("Press Z to answer another question");
        }
    }

    private void Update(){
        if (state == BattleState.PlayerAction){
            HandleActionSelection();
        }

        else if (state == BattleState.PlayerMove){
            HandleMoveSelection();
        }


    }

    void HandleActionSelection(){
        if (Input.GetKeyDown(KeyCode.Z)){
            PlayerMove();
        }
    }

    void HandleMoveSelection(){
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (currentMove < 3){
                ++currentMove;
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentMove > 0){
                --currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentMove < 2){
                currentMove+=2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentMove > 1){
                currentMove -=2;
            }
        }

        dialogBox.UpdateMoveSelection(currentMove);

        if (Input.GetKeyDown(KeyCode.Z)){
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
        
    }

}
