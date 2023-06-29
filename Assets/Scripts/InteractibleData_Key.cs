using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class InteractibleData_Key : InteractibleData
{
    [SerializeField] Key key;
    public override void GiveParametersToPlayer()
    {
        PlayerManager.Instance.Keys.Add(key);
        PlayerManager.Instance.MoveToPosition(PlayerManager.Instance.CameraPivotPosition);
        PlayerManager.Instance.InCintematic = true;
    }
}
