using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleZone : MonoBehaviour
{
    [SerializeField] bool isNull = false;
    [SerializeField] InteractibleData data;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            data.GiveParametersToPlayer();
            isNull = true;
        }
    }
}