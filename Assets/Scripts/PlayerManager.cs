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
    public bool InCintematic
    {
        get => inCintematic;
        set => inCintematic = value;
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
        inCintematic = true;
        MoveToPosition(interactible.Position);
        while (_agent.IsMoving)
        {
            yield return null;
        }
        _agent.DoAction(interactible.Rotation, interactible.Data.Height switch
        {
            InteractibleHeight.High => CharacterAction.HighInspection,
            InteractibleHeight.Low => CharacterAction.LowInspection,
            _ => throw new System.NotImplementedException()
        });
        interactible.Data.GiveParametersToPlayer(interactible);
    }

    public void ExecuteParameters()
    {
        StartCoroutine(ExecuteParametersRoutine());
    }

    private IEnumerator ExecuteParametersRoutine()
    {
        _agent.MoveToPosition(cameraPivot.position);
        while (_agent.IsMoving)
        {
            yield return null;
        }
        if (playerParameter == "")
        {
            
        }
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
