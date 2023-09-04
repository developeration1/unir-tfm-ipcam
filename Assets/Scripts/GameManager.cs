using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string ip = "0.0.0.0";
    [SerializeField] private List<string> passwords;
    [SerializeField] private List<TagToPivot> tagToPivotList;
    [SerializeField] UnityEvent<Key> OnKeyGet;

    public string IP => ip;

    public static bool EvaluateAnswer(string message)
    {
        return Instance.passwords.Contains(message);
    }

    public void TryUnlockDoors(Key key)
    {
        OnKeyGet.Invoke(key);
    }

    public bool PivotExists(string tag)
    {
        foreach (TagToPivot item in tagToPivotList)
        {
            if (item.Tag == tag)
                return true;
        }
        return false;
    }

    public Transform PivotFromTag(string tag)
    {
        foreach (TagToPivot item in tagToPivotList)
        {
            if (item.Tag == tag)
                return item.Pivot;
        }
        return null;
    }

    public Transform HandFromTag(string tag)
    {
        foreach (TagToPivot item in tagToPivotList)
        {
            if (item.Tag == tag)
                return item.Hand;
        }
        return null;
    }

    [Serializable]
    private struct TagToPivot
    {
        [SerializeField] string tag;
        [SerializeField] Transform pivot;
        [SerializeField] Transform hand;

        public string Tag => tag;
        public Transform Pivot => pivot;
        public Transform Hand => hand;
    }
}