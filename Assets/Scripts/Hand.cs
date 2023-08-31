using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] TMP_Text info;
    [SerializeField] bool isKey;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public bool IsKey
    {
        get => isKey;
        set => isKey = value;
    }

    public string Info
    {
        get => info.text;
        set => info.text = value;
    }

    public bool Showing
    {
        get => anim.GetBool("Showing");
        set => anim.SetBool("Showing", value);
    }
}
