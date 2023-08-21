using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactible Clue", menuName = "Interactibles/Clue")]
public class InteractibleData_Clue : InteractibleData
{
    [SerializeField] string clue;
    public override string GiveParametersToPlayer(Interactible owner)
    {
        Destroy(owner);
        return clue;
    }
}
