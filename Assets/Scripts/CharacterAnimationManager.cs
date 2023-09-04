using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationManager : MonoBehaviour
{
    //[SerializeField] NavMeshAgent agent;
    private Animator anim;
    private CharacterAction currentAction = CharacterAction.None;

    public CharacterAction CurrentAction => currentAction;

    public delegate void FinishActing(CharacterAction action);
    public FinishActing OnFinishedActing;

    public float Velocity
    {
        get => anim.GetFloat("Velocity");
        set => anim.SetFloat("Velocity", value);
    }

    public bool Show
    {
        get => anim.GetBool("Show");
        set
        {
            anim.SetBool("Show", value);
            currentAction = value ? CharacterAction.Showing : CharacterAction.None;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void InspectHigh()
    {
        //PlayerManager.Instance.InTransition = markAsTransition;
        currentAction = CharacterAction.HighInspection;
        anim.SetTrigger("Inspect High");
    }

    public void InspectLow()
    {
        //PlayerManager.Instance.InTransition = markAsTransition;
        currentAction = CharacterAction.LowInspection;
        anim.SetTrigger("Inspect Low");
    }

    public void Write()
    {
        //PlayerManager.Instance.InTransition = markAsTransition;
        currentAction = CharacterAction.Writing;
        anim.SetTrigger("Write");
    }

    public void ReadMessage()
    {
        currentAction = CharacterAction.ReadingMessage;
        anim.SetTrigger("Read Message");
    }

    public void Confused()
    {
        currentAction = CharacterAction.Confusing;
        anim.SetTrigger("Confused");
    }

    public void Death()
    {
        currentAction = CharacterAction.Death;
        anim.SetTrigger("Death");
    }

    public void FinishedActing(float delay = 0)
    {
        StartCoroutine(FinishedActingRoutine(delay));
    }

    private IEnumerator FinishedActingRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        //print("finished time");
        OnFinishedActing.Invoke(currentAction);
        currentAction = CharacterAction.None;
    }
}
