using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for the burdy.
public class BurdScript : MonoBehaviour
{
    //declaring the rigidbody for burdy and setting the movespeed. the movespeed can also be modified in the game.
    public Rigidbody2D myRigidbody;
    public double moveSpeed = 0.002;
    //public float flapStrength = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //whenever you press space bar, the burd will JUMP
        if(Input.GetKeyDown(KeyCode.Space)==true){
            myRigidbody.velocity = Vector2.up * 10;

        } 

        // moving the burd right by default.

        transform.position = transform.position + (Vector3.right * 4) * Time.deltaTime;

        
        
    }
}
