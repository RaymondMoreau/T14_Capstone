using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour //script for the needle (where it lands)
{
    public SpinnerLogicScript _spinner; 

    private void OnTriggerStay2D(Collider2D col) //function that determines a tile the spinner landed on
    {
        if (!_spinner.isStopped)
        {
            return;
        }
        print(col.gameObject.name);
    }
}
