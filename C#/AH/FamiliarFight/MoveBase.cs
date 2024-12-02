using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Move", menuName = "newBattlers/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string damage;

    public string Name{
        get{return name;}
    }
    public string Damage{
        get{return damage;}
    }
}
