using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private CharacterNavMeshAgent _agent;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private List<Key> keys;
    [SerializeField] private bool inCintematic;

    private string playerParameter;

    public bool IsMoving => _agent.IsMoving;
    public List<Key> Keys => keys;
    public Vector3 CameraPivotPosition => cameraPivot.position;
    public bool InCintematic => inCintematic;

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
        inCintematic = true;
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

    public void ExecuteParameters()
    {
        StartCoroutine(ExecuteParametersRoutine());
    }

    private IEnumerator ExecuteParametersRoutine()
    {
        if (playerParameter == "" || playerParameter == null)
        {
            //in case of null
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
                //add note to hand
            }
            else
            {
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
            _agent.DoAction(CharacterAction.Showing);
            yield return null;
            while (_agent.IsActing)
            {
                yield return null;
            }
        }
        
        inCintematic = false;
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
