using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private List<Key> keys;
    [SerializeField] private bool inCintematic;

    public List<Key> Keys => keys;
    public Vector3 CameraPivotPosition => cameraPivot.position;
    public bool InCintematic
    {
        get => inCintematic;
        set => inCintematic = value;
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.destination = position;
    }

    public bool HasKey(int id)
    {
        foreach (Key key in keys)
        {
            if(key.ID == id)
                return true;
        }
        return false;
    }
}
