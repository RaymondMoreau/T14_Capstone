using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this file works on the moving platforms of the Math Dragon game
public class MovingPlatform : MonoBehaviour
{

    // declaring the points A and B which is the end to end points of the platform to travel
    public Transform pointA;
    public Transform pointB;
    // the speed of the platform is set
    public float moveSpeed = 2f;

    private Vector3 nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        // platform moves towards positionB
        nextPosition = pointB.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform the position based on where its currently at, if its at A, move towards B and vice versa
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if(transform.position == nextPosition){
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
        
    }

    // make the character stay in place when its on the moving platform. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    // make the character move normally once its not on the moving platform
     private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
