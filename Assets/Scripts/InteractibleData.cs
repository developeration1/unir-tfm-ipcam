using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleData : ScriptableObject
{
    [SerializeField] InteractibleHeight height;
    public InteractibleHeight Height => height;
    public abstract string GiveParametersToPlayer(Interactible owner);
}