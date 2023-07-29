using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Animator anim;

    [SerializeField] UnityEvent OnFinishInspecting;

    public float Velocity
    {
        get => anim.GetFloat("Velocity");
        set => anim.SetFloat("Velocity", value);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void InspectHigh()
    {
        anim.SetTrigger("Inspect High");
    }

    public void InspectLow()
    {
        anim.SetTrigger("Inspect Low");
    }

    public void FinishInspecting()
    {
        OnFinishInspecting.Invoke();
    }

    public void EndCinematic()
    {
        PlayerManager.Instance.InCintematic = false;
    }
}
