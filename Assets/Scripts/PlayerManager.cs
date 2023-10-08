using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private CharacterNavMeshAgent _agent;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Hand hand;
    [SerializeField] private List<Key> keys;
    [SerializeField] private bool inCinematic;

    [SerializeField] private List<string> answers;

    private string playerParameter;

    public bool IsMoving => _agent.IsMoving;
    public Transform CameraPivot
    {
        get => cameraPivot;
        set => cameraPivot = value;
    }
    public List<Key> Keys => keys;
    public bool InCinematic => inCinematic;

    public void StartCinematic(params string[] messages)
    {
        StartCoroutine(StartCinematicRoutine(messages));
    }

    private IEnumerator StartCinematicRoutine(params string[] messages)
    {
        inCinematic = true;
        MoveToPosition(cameraPivot.position);
        yield return null;
        while (_agent.IsMoving)
        {
            yield return null;
        }
        foreach (string message in messages)
        {
            FastNote(message);
            yield return null;
            while (inCinematic)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void SetPositionToHand(Transform pivot)
    {
        hand.transform.position = pivot.position;
        hand.transform.rotation = pivot.rotation;
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.MoveToPosition(position);
    }
    
    public void Inspect(Interactible interactible)
    {
        StartCoroutine(InspectRoutine(interactible));
    }

    private IEnumerator InspectRoutine(Interactible interactible)
    {
        inCinematic = true;
        MoveToPosition(interactible.Position);
        yield return null;
        //print("Start Moving");
        while (_agent.IsMoving)
        {
            //print("Still Moving");
            yield return null;
        }
        //print("Finished Moving");
        _agent.DoAction(interactible.Rotation, interactible.Data.Height switch
        {
            InteractibleHeight.High => CharacterAction.HighInspection,
            InteractibleHeight.Low => CharacterAction.LowInspection,
            _ => throw new System.NotImplementedException()
        });
        playerParameter = interactible.Data.GiveParametersToPlayer(interactible);
    }

    public void FastNote(string note)
    {
        StartCoroutine(FastNoteRoutine(note));
    }

    private IEnumerator FastNoteRoutine(string note)
    {
        inCinematic = true;
        hand.IsKey = false;
        hand.Info = note;
        _agent.DoAction(CharacterAction.Writing);
        while (_agent.IsActing)
        {
            yield return null;
        }
        yield return null;
        _agent.MoveToPosition(cameraPivot.position);
        yield return null;
        while (_agent.IsMoving)
        {
            yield return null;
        }
        yield return null;
        _agent.DoAction(cameraPivot.rotation, CharacterAction.Showing);
        yield return null;
        while (_agent.IsActing)
        {
            yield return null;
        }
        yield return null;
        hand.Showing = true;
        print("showing");
        while (_agent.IsActing)
        {
            yield return null;
        }
        yield return new WaitForSeconds(7);
        hand.Showing = false;
        inCinematic = false;

    }

    public void EndingNote(string note, Transform finalPivot)
    {
        StartCoroutine(EndingNoteRoutine(note, finalPivot));
    }

    private IEnumerator EndingNoteRoutine(string note, Transform finalPivot)
    {
        inCinematic = true;
        hand.IsKey = false;
        hand.Info = note;
        _agent.DoAction(CharacterAction.Writing);
        while (_agent.IsActing)
        {
            yield return null;
        }
        yield return null;
        _agent.MoveToPosition(cameraPivot.position);
        yield return null;
        while (_agent.IsMoving)
        {
            yield return null;
        }
        yield return null;
        _agent.DoAction(cameraPivot.rotation, CharacterAction.Showing);
        yield return null;
        while (_agent.IsActing)
        {
            yield return null;
        }
        yield return null;
        hand.Showing = true;
        print("showing");
        while (_agent.IsActing)
        {
            yield return null;
        }
        yield return new WaitForSeconds(7);
        hand.Showing = false;
        MoveToPosition(finalPivot.position);
    }

    public void ExecuteParameters()
    {
        StartCoroutine(ExecuteParametersRoutine());
    }

    public void Die()
    {
        _agent.DoAction(CharacterAction.Death);
        inCinematic = true;
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(4);
        Application.Quit();
    }

    private IEnumerator ExecuteParametersRoutine()
    {
        if (playerParameter == "" || playerParameter == null)
        {
            //in case of null
            yield return null;
            _agent.DoAction(CharacterAction.Confusing);
            while (_agent.IsActing)
            {
                yield return null;
            }
        }
        else
        {
            if (playerParameter != "key")
            {
                //in case of clue or hint
                yield return null;
                _agent.DoAction(CharacterAction.Writing);
                while (_agent.IsActing)
                {
                    yield return null;
                }
                hand.IsKey = false;
                hand.Info = playerParameter;
                //add note to hand
            }
            else
            {
                hand.IsKey = true;
                GameManager.Instance.TryUnlockDoors(Keys[^1]);
                //in case of key
                //add key to hand
            }
            yield return null;
            _agent.MoveToPosition(cameraPivot.position);
            yield return null;
            while (_agent.IsMoving)
            {
                yield return null;
            }
            yield return null;
            _agent.DoAction(cameraPivot.rotation, CharacterAction.Showing);
            yield return null;
            hand.Showing = true;
            print("showing");
            while (_agent.IsActing)
            {
                yield return null;
            }
            yield return new WaitForSeconds(7);
            hand.Showing = false;
        }
        
        inCinematic = false;
    }

    public void ReceiveAnswer(string message)
    {
        message = message.ToUpper();
        StartCoroutine(ReadAnswerRoutine(message));
    }

    public IEnumerator ReadAnswerRoutine(string message)
    {
        inCinematic = true;
        if (GameManager.EvaluateAnswer(message))
        {
            answers.Add(message);
        }
        _agent.DoAction(CharacterAction.ReadingMessage);
        yield return null;
        while (_agent.IsActing)
        {
            yield return null;
        }
        inCinematic = false;
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

    public bool HasAnswer(string answer)
    {
        return answers.Contains(answer);
    }
}
