using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string ip = "0.0.0.0";
    [SerializeField] private List<string> passwords;

    public string IP => ip;

    public static bool EvaluateAnswer(string message)
    {
        message = message.ToLower();
        return Instance.passwords.Contains(message);
    }
}
