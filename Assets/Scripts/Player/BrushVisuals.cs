using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BrushVisuals : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _brushRigidbody2D;
    [SerializeField]
    private Animator _animator;

    private Brush _brush;

    private readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _brush = _brushRigidbody2D.GetComponent<Brush>();
    }

    private void Update()
    {
        _animator.SetBool(IsMoving, _brush.IsMoving);
    }
}
