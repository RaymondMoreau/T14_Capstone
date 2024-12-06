using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for managing and animating the health bar of a battler
public class HPBar : MonoBehaviour
{
    [SerializeField] public GameObject health; // Reference to the health bar GameObject (visual representation)

    // Sets the health bar's scale based on the current HP (normalized to a value between 0 and 1)
    public void SetHP(float hpNormalized)
    {
        // Adjust the scale of the health bar along the X-axis to reflect the current HP percentage
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    // Coroutine to smoothly animate the health bar's transition to a new HP value
    public IEnumerator SetHPSmooth(float newHp)
    {
        float curHp = health.transform.localScale.x; // Get the current HP as represented by the health bar's X-scale
        float changeAmt = curHp - newHp; // Calculate the total change needed in the health bar

        // Gradually reduce the health bar's scale until it matches the new HP value
        while (curHp - newHp > Mathf.Epsilon) // Continue until the difference is negligible (Mathf.Epsilon handles precision issues)
        {
            curHp -= changeAmt * Time.deltaTime; // Smoothly decrease the current HP value over time
            health.transform.localScale = new Vector3(curHp, 1f); // Update the health bar's scale
            yield return null; // Wait until the next frame
        }

        // Ensure the final scale exactly matches the target HP value
        health.transform.localScale = new Vector3(newHp, 1f);
    }
}