using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerLogicScript : MonoBehaviour //Logic script for the wheel spinner (this game is not fully implemented yet)
{
    public float reducer;
    public float multiplier;
    bool round1 = false;
    public bool isStopped = false;
    
    // Start is called before the first frame update
    void Start() 
    {
        reducer = Random.Range(0.01f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate() //Determines strength of the spin
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Reset();
        }

        if(multiplier > 0)
        {
            transform.Rotate(Vector3.forward, 1 * multiplier);
        }
        else
        {
            isStopped = true;
        }

        if(multiplier <20 && !round1)
        {
            multiplier += 0.2f;
        }
        else
        {
            round1 = true;
        }

        if(round1 && multiplier > 0)
        {
            multiplier -= reducer;
        }
    }

    private void Reset() //resets all values for a new spin
    {
        multiplier = 1;
        reducer = Random.Range(0.1f, 0.5f);
        round1 = false;
        isStopped = false;
    }
}
