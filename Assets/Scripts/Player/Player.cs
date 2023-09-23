using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Actor
{
    [Header("Input")]
    [SerializeField]
    private InputReader _inputReader;

    [Header("References")]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private Transform _feetPosition;
    [SerializeField]
    private Rigidbody2D _brushRigidbody2D;
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
    private bool _isGrounded = false;

    private bool _isBrushThrown = false;

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
        }
        else
        {
            _isBrushThrown = false;
            _brushRigidbody2D.isKinematic = true;
            _brushRigidbody2D.velocity = Vector3.zero;
            _brushRigidbody2D.transform.SetParent(_brushAnchor);
            _brushRigidbody2D.transform.localPosition = Vector3.zero;
        }
    }

    private void OnJumpPerformed()
    {
        if (!_isGrounded) return;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
    }

    private void OnMoveCanceled()
    {
        _moveDirection = 0f;
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
    }

    protected override void Update()
    {
        float radius = 0.1f;
        _isGrounded = Physics2D.OverlapCircle(_feetPosition.position, radius, _groundLayerMask);

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
    }
}
