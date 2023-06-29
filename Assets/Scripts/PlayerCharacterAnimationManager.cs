using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimationManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Velocity", agent.velocity.magnitude);
    }

}
