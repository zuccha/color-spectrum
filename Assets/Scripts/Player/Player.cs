using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private InputReader _inputReader;

    [Header("References")]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private Transform _feetPosition;

    [Header("Customization")]
    [SerializeField]
    private LayerMask _groundLayerMask;
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _jumpForce = 10f;

    private float _moveDirection = 0f;
    private bool _isGrounded = false;

    private void Reset()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += OnMove;
        _inputReader.MoveCanceled += OnStopMove;
        _inputReader.JumpPerformed += OnJump;
    }

    private void OnJump()
    {
        if (!_isGrounded) return;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= OnMove;
        _inputReader.MoveCanceled -= OnStopMove;
    }

    private void OnStopMove()
    {
        _moveDirection = 0f;
    }

    private void OnMove(float direction)
    {
        _moveDirection = direction;
        if (direction > 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = 1;
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -1;
            transform.localScale = localScale;
        }
    }

    private void Update()
    {
        float radius = 0.1f;
        _isGrounded = Physics2D.OverlapCircle(_feetPosition.position, radius, _groundLayerMask);

        Debug.Log(_isGrounded);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_moveDirection * _moveSpeed, _rigidbody2D.velocity.y);
    }
}
