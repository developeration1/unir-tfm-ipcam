using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavMeshAgentCustom : MonoBehaviour
{
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    private void LateUpdate()
    {
        if (_agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized), .1f);
        }
    }
}
