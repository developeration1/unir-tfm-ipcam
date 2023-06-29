using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] GameObject key;

    public int ID => id;
}
