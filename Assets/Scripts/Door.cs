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

    private void Start()
    {
        obstacle.enabled = keyReference != null;
    }

    public void TryUnlock(Key key)
    {
        if(keyReference != null)
        {
            if (keyReference == key)
                obstacle.enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.CompareTag("Player") && !obstacle.enabled)
        {
            animator.SetBool("Open", true);
        }
        if (other.CompareTag("Enemy"))
        {
            animator.SetBool("Open", true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !obstacle.enabled)
        {
            animator.SetBool("Open", false);
        }
        if (other.CompareTag("Enemy"))
        {
            animator.SetBool("Open", false);
            print("cierra puerta enemigo");
        }
    }
}
