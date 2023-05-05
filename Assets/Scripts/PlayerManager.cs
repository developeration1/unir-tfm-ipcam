using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private NavMeshAgent _agent;

    public void MoveToPosition(Vector3 position)
    {
        _agent.destination = position;
    }
}
