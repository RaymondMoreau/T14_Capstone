using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBobbingScript : MonoBehaviour
{
    public float bobbingSpeed = 2.0f;
    public float bobbingAmount = 0.1f;
    private float startY;
    /*
     * this script is for animating the floating of the ships but is not currently being used
     * 
     */

    public GameObject enemyShip1, enemyShip2, enemyShip3;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update() 
    {
        float newY = startY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
