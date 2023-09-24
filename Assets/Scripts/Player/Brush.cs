using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushPaint
{
    Blue,
    Black,
    Green,
    Red,
}

public enum BrushState { Held, Thrown, Stuck, Returning }

public class Brush : MonoBehaviour
{
    public BrushState State { get; private set; } = BrushState.Held;

    public event Action<Color> BrushColorAdded;

    public BrushPaint Paint
    {
        get
        {
            return 0 <= _selectedBrushColorIndex && _selectedBrushColorIndex < _availableBrushColors.Count
                ? _availableBrushColors[_selectedBrushColorIndex]
                : BrushPaint.Black;
        }
    }

    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Transform _anchor;

    [SerializeField]
    private float _maxBrushDistance = 15f;

    [SerializeField]
    private float _maxBrushThrowDistance = 4f;

    [SerializeField]
    private float _minBrushDistance = 0.5f;

    [SerializeField]
    private float _returnSpeed = 20;

    [SerializeField]
    private SpriteRenderer _brushTipSpriteRenderer;

    [SerializeField]
    private float _cameraShakeIntensity = 0.5f;
    [SerializeField]
    private float _cameraShakeDuration = 0.25f;

    private int _selectedBrushColorIndex = 0;

    [SerializeField]
    private List<BrushPaint> _availableBrushColors = new List<BrushPaint> { BrushPaint.Black };

    private PaintOutline _lastTouchedPaintOutline;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _paintClip;
    [SerializeField]
    private AudioClip _throwClip;

    public bool IsMoving { get; private set; } = false;

    public List<BrushPaint> GetAvailableBrushColors()
    {
        return _availableBrushColors;
    }

    public static Color ColorByPaint(BrushPaint paint)
    {
        switch (paint)
        {
            case BrushPaint.Blue: return Color.blue;
            case BrushPaint.Black: return Color.black;
            case BrushPaint.Green: return Color.green;
            case BrushPaint.Red: return Color.red;
        }
        return Color.white;
    }

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _brushTipSpriteRenderer.color = ColorByPaint(Paint);
    }

    private void Update()
    {
        IsMoving = State == BrushState.Thrown || State == BrushState.Returning;

        if ((_rigidbody2D.transform.position - _player.transform.position).magnitude > _maxBrushDistance)
            Free();

        if (State == BrushState.Thrown &&
            (_rigidbody2D.transform.position - _player.transform.position).magnitude > _maxBrushThrowDistance)
            Free();

        if (State == BrushState.Returning)
        {
            if ((_rigidbody2D.transform.position - _player.transform.position).magnitude < _minBrushDistance)
            {
                PutBack();
            }
            else
            {
                float maxDistance = _returnSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, _anchor.position, maxDistance);
            }
        }
    }

    public void Throw(float direction, float strength)
    {
        if (State != BrushState.Held) return;

        State = BrushState.Thrown;
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.transform.SetParent(null);
        _rigidbody2D.AddForce(new Vector2(direction, 0f) * strength, ForceMode2D.Impulse);
        _boxCollider2D.enabled = true;

        AudioManager.Instance.PlayEffect(_throwClip);
    }

    public void Stuck(PaintOutline paintOutline)
    {
        if (paintOutline == _lastTouchedPaintOutline) return;

        State = BrushState.Stuck;
        _rigidbody2D.velocity = Vector2.zero;
        _lastTouchedPaintOutline = paintOutline;
        CameraShake.Instance.ShakeCamera(_cameraShakeIntensity, _cameraShakeDuration);

        AudioManager.Instance.PlayEffect(_paintClip);
    }

    public void Free()
    {
        if (State != BrushState.Thrown && State != BrushState.Stuck) return;
        _rigidbody2D.velocity = Vector2.zero;

        if (State == BrushState.Stuck)
        {
            CameraShake.Instance.ShakeCamera(_cameraShakeIntensity, _cameraShakeDuration);
        }

        State = BrushState.Returning;

        AudioManager.Instance.PlayEffect(_throwClip);
    }

    public void PutBack()
    {
        State = BrushState.Held;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.transform.SetParent(_anchor);
        _rigidbody2D.transform.localPosition = Vector3.zero;
        transform.rotation = _player.transform.rotation;
        _boxCollider2D.enabled = false;
        _lastTouchedPaintOutline = null;
    }

    public void SwitchBrushColorLeft()
    {
        if (State != BrushState.Held) return;

        --_selectedBrushColorIndex;
        if (_selectedBrushColorIndex < 0) _selectedBrushColorIndex = _availableBrushColors.Count - 1;
        _brushTipSpriteRenderer.color = ColorByPaint(Paint);
    }

    public void SwitchBrushColorRight()
    {
        if (State != BrushState.Held) return;

        _selectedBrushColorIndex = (_selectedBrushColorIndex + 1) % _availableBrushColors.Count;
        _brushTipSpriteRenderer.color = ColorByPaint(Paint);
    }

    public void AddPaint(BrushPaint paint, bool autoSelect)
    {
        if (_availableBrushColors.Contains(paint)) return;
        _availableBrushColors.Add(paint);
        if (autoSelect)
        {
            _selectedBrushColorIndex = _availableBrushColors.Count - 1;
            _brushTipSpriteRenderer.color = ColorByPaint(Paint);
        }

        BrushColorAdded?.Invoke(ColorByPaint(paint));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor) return;

        Mouth mouth = other.GetComponent<Mouth>();
        if (mouth) return;

        PaintOutline paintOutline = other.GetComponent<PaintOutline>();
        if (paintOutline) return;

        Free();
    }
}
