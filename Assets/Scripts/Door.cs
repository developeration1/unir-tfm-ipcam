using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] Key keyReference;
    [SerializeField] NavMeshObstacle obstacle;
    [SerializeField] Animator animator;
    [SerializeField] Transform cameraPivot;
    [SerializeField] Transform handPivot;
    [SerializeField] string hint;

    private void Start()
    {
        obstacle.enabled = keyReference != null;
    }

    public void TryUnlock(Key key)
    {
        if (keyReference != null)
        {
            if (keyReference == key)
                obstacle.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && obstacle.enabled)
        {
            PlayerManager.Instance.SetPositionToHand(handPivot);
            PlayerManager.Instance.CameraPivot = cameraPivot;
            PlayerManager.Instance.FastNote(hint);
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            animator.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") && !obstacle.enabled) || other.CompareTag("Enemy"))
        {
            animator.SetBool("Open", false);
        }
    }
}
