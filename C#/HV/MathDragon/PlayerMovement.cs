using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// about player movement.
public class PlayerMovement : MonoBehaviour
{
    //declare character's rigidbody object
    Rigidbody2D rb;
    //float moveForce=2;
    //variables for character's speed and jump power
    [SerializeField] private float jumpPower;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // if it gets horizontal arrow (right, left) key input it moves accordingly. moves left if left arrow key. moves right if right arrow key.
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        //rb.AddForce(transform.right*moveForce*Input.GetAxis("Horizontal"), ForceMode2D.Force);
        // for jumping, space bar makes the character jump. Jump code is implemented below in Jump()
        if(Input.GetKey(KeyCode.Space))
        { 
            Jump();
            }
        
    }

    private void Jump()
    {
        //Jump implementation. Jumppower is multiplied by the current horizontal velocity.
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            
    
     


    }


    
}
