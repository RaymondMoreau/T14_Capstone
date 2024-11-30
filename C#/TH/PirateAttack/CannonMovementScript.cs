using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovementScript : MonoBehaviour
{
    public LogicScript logic;
    public EnemyShipSpawner enemySpawner;
    public float moveSpeed;
    public float deadzone = 15;
    

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyShipSpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;

        if (transform.position.x > deadzone)
        {
            //Debug.Log("cannon deleted");
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) //function that destroys cannon ball when it hits enemy ship
    {
        //Debug.Log("hit");
        Destroy(gameObject);
        logic.firedState = false;
    }
}
