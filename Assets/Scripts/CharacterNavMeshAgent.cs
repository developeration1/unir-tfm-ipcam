using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField] private float moveVel = 0.5f;

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
        animationManager.Velocity = Mathf.Lerp(animationManager.Velocity, _agent.velocity.magnitude, moveVel);
    }

    private void LateUpdate()
    {
        if (_agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized), moveVel);
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
            case CharacterAction.Death:
                animationManager.Death();
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
    Confusing,
    Death
}