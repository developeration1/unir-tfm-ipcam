using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleZone : MonoBehaviour
{
    [SerializeField] bool isNull = false;
    [SerializeField] InteractibleData data;
    [SerializeField] Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.InCintematic = true;
            StartCoroutine(WaitForVelocity());
        }
    }

    private IEnumerator WaitForVelocity()
    {
        while (!PlayerManager.Instance.IsMoving)
        {
            yield return null;
        }
        //PlayerManager.Instance.Inspect(destination.rotation, data.GiveParametersToPlayer);
    }
}