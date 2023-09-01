using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem.UI;

[CreateAssetMenu(fileName = "Interactible Key", menuName = "Interactibles/Key")]
public class InteractibleData_Key : InteractibleData
{
    [SerializeField] Key key;
    [SerializeField] string password;
    [SerializeField] string passwordHint;

    public override string GiveParametersToPlayer(Interactible owner)
    {
        if(password == null || password == "" || PlayerManager.Instance.HasAnswer(password))
        {
            PlayerManager.Instance.Keys.Add(key);
            Destroy(owner);
            return "key";
        }
        return passwordHint;
    }
}
