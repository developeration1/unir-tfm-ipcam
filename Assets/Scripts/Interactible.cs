using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] InteractibleData data;
    [SerializeField] Transform destination;

    public Vector3 Position => destination.position;
    public Quaternion Rotation => destination.rotation;
    public InteractibleData Data => data;
}
