using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMenuItem : MonoBehaviour
{
    public bool IsSelected { get; private set; }

    private Animator _animator;
    private TMPro.TMP_Text _text;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponent<TMPro.TMP_Text>();
    }

    public void Select()
    {
        IsSelected = true;
        _animator.SetBool("IsSelected", true);
        _text.color = Color.magenta;
    }

    public void Deselect()
    {
        IsSelected = false;
        _animator.SetBool("IsSelected", false);
        _text.color = Color.black;
    }

    public abstract void Activate();
}
