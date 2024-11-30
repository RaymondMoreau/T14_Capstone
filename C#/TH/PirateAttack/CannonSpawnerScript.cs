using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CannonSpawnerScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject cannon;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space) && logic.firedState == false)
        {
            fireCannon();
            logic.firedState = true;
        }
    }

    void fireCannon() // function that fires ally cannon
    {
        Instantiate(cannon, transform.position, transform.rotation);
    }
}
