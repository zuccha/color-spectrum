using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Actor
{
    public event Action PlayerMoves;
    public event Action PlayerIdles;
    public event Action PlayerJumps;
    public event Action PlayerChangedDirection;

    [Header("Input")]
    [SerializeField]
    private InputReader _inputReader;

    [Header("References")]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private Transform _feetPosition;
    [SerializeField]
    private Brush _brush;

    [Header("Customization")]
    [SerializeField]
    private LayerMask _groundLayerMask;
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _jumpForce = 10f;
    [SerializeField]
    private float _throwStrength = 10f;
    [SerializeField]
    private bool _hasBrush = true;

    private float _moveDirection = 0f;
    private float _throwDirection = 1f;
    public bool IsGrounded { get; private set; } = false;

    public void AddBrushPaint(BrushPaint paint)
    {
        _brush.AddPaint(paint, _brush.State == BrushState.Held);
    }

    public override void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetHasBrush(bool hasBrush)
    {
        _hasBrush = hasBrush;
        _brush.gameObject.SetActive(hasBrush);
    }

    private void Reset()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        _brush.gameObject.SetActive(_hasBrush);
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += OnMovePerformed;
        _inputReader.MoveCanceled += OnMoveCanceled;
        _inputReader.JumpPerformed += OnJumpPerformed;
        _inputReader.ThrowBrushPerformed += OnThrowBrushPerformed;
        _inputReader.SwitchBrushColorLeft += OnSwitchBrushColorLeft;
        _inputReader.SwitchBrushColorRight += OnSwitchBrushColorRight;
    }

    private void OnThrowBrushPerformed()
    {
        if (!_hasBrush) return;

        if (_brush.State == BrushState.Held)
            _brush.Throw(_throwDirection, _throwStrength);
        else
            _brush.Free();
    }

    private void OnSwitchBrushColorLeft()
    {
        if (!_hasBrush) return;
        _brush.SwitchBrushColorLeft();
    }

    private void OnSwitchBrushColorRight()
    {
        if (!_hasBrush) return;
        _brush.SwitchBrushColorRight();
    }

    private void OnJumpPerformed()
    {
        if (IsGrounded)
        {
            PlayerJumps?.Invoke();
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }
        else if (_materialType == MaterialType.Lava)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce / 4);
        }
        else if (_materialType == MaterialType.Water)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce / 2);
        }
    }

    private void OnMoveCanceled()
    {
        _moveDirection = 0f;
        PlayerIdles?.Invoke();
    }

    private void OnMovePerformed(float direction)
    {
        if (direction > 0)
        {
            _moveDirection = 1;
            _throwDirection = 1;
            Quaternion localRotation = transform.localRotation;
            localRotation.y = 0;
            transform.localRotation = localRotation;

            if (IsGrounded) PlayerChangedDirection?.Invoke();
        }
        else
        {
            _moveDirection = -1;
            _throwDirection = -1;
            Quaternion localRotation = transform.localRotation;
            localRotation.y = 180;
            transform.localRotation = localRotation;
            if (IsGrounded) PlayerChangedDirection?.Invoke();
        }

        PlayerMoves?.Invoke();
    }

    protected override void Update()
    {
        float radius = 0.1f;
        IsGrounded = Physics2D.OverlapCircle(_feetPosition.position, radius, _groundLayerMask);
        base.Update();
    }

    protected override void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_moveDirection * _moveSpeed, _rigidbody2D.velocity.y);
        base.FixedUpdate();
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= OnMovePerformed;
        _inputReader.MoveCanceled -= OnMoveCanceled;
        _inputReader.JumpPerformed -= OnJumpPerformed;
        _inputReader.ThrowBrushPerformed -= OnThrowBrushPerformed;
        _inputReader.SwitchBrushColorLeft -= OnSwitchBrushColorLeft;
        _inputReader.SwitchBrushColorRight -= OnSwitchBrushColorRight;
    }
}
