using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShipMovement : MonoBehaviour
{
    public GameObject allyShip;
    public LogicScript logic;


    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        moveUp();
        moveDown();
       
    }

    public void moveUp() //function to move ally ship upwards
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (transform.position.y != 3) && logic.firedState == false){
            transform.position = transform.position + (Vector3.up * 3);
            
        }
    }

    public void moveDown() //function to move ally ship downwards
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && (transform.position.y != -3) && logic.firedState == false)
        {
            transform.position = transform.position + (Vector3.down * 3);
        }
    }

}
