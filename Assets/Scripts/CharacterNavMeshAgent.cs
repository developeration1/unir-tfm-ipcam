using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterNavMeshAgent : MonoBehaviour
{
    [SerializeField] CharacterAnimationManager animationManager;

    //[SerializeField] UnityEvent OnFinishInspecting;
    //[SerializeField] UnityEvent OnFinishWriting;

    private NavMeshAgent _agent;
    private bool _isMoving;

    public bool IsMoving => _isMoving;

    public bool IsActing => animationManager.CurrentAction != CharacterAction.None;

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        animationManager.OnFinishedActing += FinishedActing;
    }

    private void Update()
    {
        //print(IsMoving);
        _isMoving = _agent.remainingDistance > .001f; //_agent.velocity.sqrMagnitude > .01;
        animationManager.Velocity = Mathf.Lerp(animationManager.Velocity, _agent.velocity.magnitude, .1f);
    }

    private void LateUpdate()
    {
        if (_agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized), .1f);
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.destination = position;
    }

    public void DoAction(Quaternion rotation, CharacterAction animation = CharacterAction.None)
    {
        _agent.transform.DORotate(rotation.eulerAngles, 1).SetEase(Ease.InOutCubic).OnComplete(() => DoAction(animation));
    }

    public void DoAction(CharacterAction animation = CharacterAction.None)
    {
        switch (animation)
        {
            case CharacterAction.None:
                break;
            case CharacterAction.HighInspection:
                animationManager.InspectHigh();
                break;
            case CharacterAction.LowInspection:
                animationManager.InspectLow();
                break;
            case CharacterAction.Writing:
                animationManager.Write();
                break;
            case CharacterAction.Showing:
                animationManager.Show = true;
                break;
            case CharacterAction.ReadingMessage:
                animationManager.ReadMessage();
                break;
            case CharacterAction.Confusing:
                animationManager.Confused();
                break;
            default:
                break;
        }
    }

    public void FinishedActing(CharacterAction action)
    {
        if (action == CharacterAction.HighInspection || action == CharacterAction.LowInspection)
            PlayerManager.Instance.ExecuteParameters();
        if (action == CharacterAction.Showing)
            animationManager.Show = false;
    }
}

public enum CharacterAction
{
    None,
    HighInspection,
    LowInspection,
    Writing,
    Showing,
    ReadingMessage,
    Confusing
}