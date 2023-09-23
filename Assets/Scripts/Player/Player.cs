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
    [SerializeField]
    private Rigidbody2D _brushRigidbody2D;
    [SerializeField]
    private BoxCollider2D _brushBoxCollider2D;
    [SerializeField]
    private Transform _brushAnchor;

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
    private float _maxBrushDistance = 4f;

    private float _moveDirection = 0f;
    private float _throwDirection = 1f;
    public bool IsGrounded { get; private set; } = false;

    private bool _isBrushThrown = false;

    public void AddBrushPaint(BrushPaint paint)
    {
        _brush.AddPaint(paint, !_isBrushThrown);
    }

    public override void Kill()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    private void Reset()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        if (!_isBrushThrown)
        {
            _isBrushThrown = true;
            _brushRigidbody2D.isKinematic = false;
            _brushRigidbody2D.gravityScale = 0f;
            _brushRigidbody2D.transform.SetParent(null);
            _brushRigidbody2D.AddForce(new Vector2(_throwDirection, 0f) * _throwStrength, ForceMode2D.Impulse);
            _brushBoxCollider2D.enabled = true;
        }
        else
        {
            _isBrushThrown = false;
            _brushRigidbody2D.isKinematic = true;
            _brushRigidbody2D.velocity = Vector3.zero;
            _brushRigidbody2D.transform.SetParent(_brushAnchor);
            _brushRigidbody2D.transform.localPosition = Vector3.zero;
            _brushBoxCollider2D.enabled = false;
        }
    }

    private void OnSwitchBrushColorLeft()
    {
        if (!_isBrushThrown) _brush.SwitchBrushColorLeft();
    }

    private void OnSwitchBrushColorRight()
    {
        if (!_isBrushThrown) _brush.SwitchBrushColorRight();
    }

    private void OnJumpPerformed()
    {
        if (IsGrounded)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        else if (_materialType == MaterialType.Lava)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce / 4);
        else if (_materialType == MaterialType.Water)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce / 2);

        PlayerJumps?.Invoke();
    }

    private void OnMoveCanceled()
    {
        _moveDirection = 0f;
        PlayerIdles?.Invoke();
    }

    private void OnMovePerformed(float direction)
    {
        _moveDirection = direction;
        _throwDirection = direction;
        if (direction > 0)
        {
            Quaternion localRotation = transform.localRotation;
            localRotation.y = 0;
            transform.localRotation = localRotation;
        }
        else
        {
            Quaternion localRotation = transform.localRotation;
            localRotation.y = 180;
            transform.localRotation = localRotation;
        }

        PlayerMoves?.Invoke();
    }

    protected override void Update()
    {
        float radius = 0.1f;
        IsGrounded = Physics2D.OverlapCircle(_feetPosition.position, radius, _groundLayerMask);

        if ((_brushRigidbody2D.transform.position - transform.position).magnitude > _maxBrushDistance)
        {
            _brushRigidbody2D.velocity = Vector2.zero;
        }

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
