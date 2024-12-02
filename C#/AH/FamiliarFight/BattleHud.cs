using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] public Text nameText;
    [SerializeField] public HPBar hpBar;

    Battler _battler;

    public void SetData(Battler battler) {
        _battler = battler;

        nameText.text = battler.Base.Name;
        hpBar.SetHP((float) battler.HP/battler.MaxHP);
    }

    public IEnumerator UpdateHP(){
        yield return hpBar.SetHPSmooth((float) _battler.HP/_battler.MaxHP);
    }
}
