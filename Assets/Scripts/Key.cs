using UnityEngine;

[CreateAssetMenu(fileName = "Key")]
public class Key : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] GameObject key;

    public int ID => id;
    public GameObject KeyItself => key;
}
