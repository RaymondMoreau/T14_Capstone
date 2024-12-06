using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class responsible for managing the Battle HUD (Heads-Up Display)
public class BattleHud : MonoBehaviour
{
    [SerializeField] public Text nameText; // UI text element for displaying the battler's name
    [SerializeField] public HPBar hpBar;  // Reference to the HPBar component to manage health display

    private Battler _battler; // Private reference to the associated battler object

    // Sets up the HUD with data from a battler object
    public void SetData(Battler battler) {
        _battler = battler; // Store the reference to the battler

        // Update the name text in the HUD
        nameText.text = battler.Base.Name;

        // Set the HP bar to reflect the battler's current health as a fraction of max health
        hpBar.SetHP((float) battler.HP / battler.MaxHP);
    }

    // Coroutine to smoothly update the HP bar when the battler's health changes
    public IEnumerator UpdateHP(){
        // Use the HPBar's SetHPSmooth function to visually transition the health bar
        yield return hpBar.SetHPSmooth((float) _battler.HP / _battler.MaxHP);
    }
}