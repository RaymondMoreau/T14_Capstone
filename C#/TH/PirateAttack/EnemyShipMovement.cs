using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    public LogicScript logic;
    public float moveSpeed;
    public float deadzone = -15;
    private int lane;
    private int answerLane;

    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 2;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        //lane = logic.lane;
        answerLane = logic.answerLane;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (logic.hitState == true) //checks when enemy ships are hit
        {
            answerLane = logic.correctLane();
            //Debug.Log("speed boost");
            //Destroy(gameObject);
            moveSpeed += 15;
            logic.hitState = false;
        }
        if(transform.position.x < -10) //checks when ship go past ally ship
        {
            Destroy(gameObject);
            logic.spawnShip();
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) // function that checks answer correctedness
    {
        logic.hitState = true;
        Debug.Log(answerLane);
        Debug.Log(gameObject.layer / 3);
        if (gameObject.layer/3 == answerLane)
        {
            Debug.Log("correct");
            Destroy(gameObject);
            logic.increaseSpeed();
            moveSpeed = 15;
            logic.score += 1;
            logic.randomizeQuestion();
        }
        else
        {
            Debug.Log("Incorrect");
            moveSpeed = 15;
            logic.lives -= 1;
            logic.randomizeQuestion();
        }
        
    }
}
