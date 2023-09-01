using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] TMP_Text info;
    [SerializeField] bool isKey;
    [SerializeField] Transform keyTransform;
    [SerializeField] Transform noteTransform;
    [SerializeField] GameObject keyInstance;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public bool IsKey
    {
        get => isKey;
        set
        {
            isKey = value;
            keyTransform.gameObject.SetActive(value);
            noteTransform.gameObject.SetActive(!value);
            if(PlayerManager.Instance.Keys.Count > 0)
            {
                if (keyInstance != null)
                    Destroy(keyInstance);
                keyInstance = Instantiate(PlayerManager.Instance.Keys[PlayerManager.Instance.Keys.Count - 1].KeyItself, keyTransform);
            }
        }
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
