using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerLogicScript : MonoBehaviour //Logic script for the wheel spinner (this game is not fully implemented yet)
{

    public float spinSpeed;
    public float deceleration = 0.5f;
    private bool isSpinning = false;
    private float currentSpeed;
    public string[] prizes;
    public int numberOfPrizes = 8;

    // Start is called before the first frame update
    void Start() 
    {
        prizes = new string[] { "Prize 1", "Prize 2", "Prize 3", "Prize 4", 
            "Prize 5", "Prize 6", "Prize 7", "Prize 8" };


    }

    // Update is called once per frame
    void Update() //Determines strength of the spin
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpinning) {
            StartSpin();
        }    

        if (isSpinning)
        {
            SpinWheel();
        }
    }

    void StartSpin()
    {
        isSpinning = true;
        currentSpeed = spinSpeed;
    }

    void SpinWheel()
    {
        transform.Rotate(0, 0, currentSpeed * Time.deltaTime);

        currentSpeed -= deceleration * Time.deltaTime;

        if(currentSpeed <= 0)
        {
            currentSpeed = 0;
            isSpinning=false;
            DetermineSection();
        }
    }

    void DetermineSection()
    {
        float angle = transform.eulerAngles.z;
        int section = Mathf.FloorToInt((angle / 360) * numberOfPrizes);

        Debug.Log("Wheel landed on section: " + (section + 1));
    }
}
