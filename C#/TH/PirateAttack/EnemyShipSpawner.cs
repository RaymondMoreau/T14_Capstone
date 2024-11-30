using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public GameObject enemyShips;
    public GameObject triggerPoint;

    // Start is called before the first frame update
    void Start()
    {
        enemyShips.GetComponent<EnemyShipMovement>().moveSpeed = 2;
        spawnShips();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      
    public void spawnShips() //function that spawns enemy ships
    {
        Instantiate(enemyShips, transform.position, transform.rotation);
        //enemyShips.GetComponent<EnemyShipMovement>().moveSpeed += 1;
    }

}
