using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushPaint
{
    Blue,
    Brown,
    Green,
    Red,
}

public enum BrushState { Held, Thrown, Stuck, Returning }

public class Brush : MonoBehaviour
{
    public BrushState State { get; private set; } = BrushState.Held;

    public BrushPaint Paint
    {
        get
        {
            return 0 <= _selectedBrushColorIndex && _selectedBrushColorIndex < _availableBrushColors.Count
                ? _availableBrushColors[_selectedBrushColorIndex]
                : BrushPaint.Brown;
        }
    }

    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Transform _anchor;

    [SerializeField]
    private float _maxBrushDistance = 4f;

    [SerializeField]
    private float _minBrushDistance = 0.5f;

    [SerializeField]
    private SpriteRenderer _brushTipSpriteRenderer;

    private int _selectedBrushColorIndex = 0;

    [SerializeField]
    private List<BrushPaint> _availableBrushColors = new List<BrushPaint> { BrushPaint.Brown };

    private PaintOutline _lastTouchedPaintOutline;

    public bool IsMoving { get; private set; } = false;

    public static Color ColorByPaint(BrushPaint paint)
    {
        switch (paint)
        {
            case BrushPaint.Blue: return Color.blue;
            case BrushPaint.Brown: return Color.yellow;
            case BrushPaint.Green: return Color.green;
            case BrushPaint.Red: return Color.red;
        }
        return Color.yellow;
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

        if (State == BrushState.Thrown &&
            (_rigidbody2D.transform.position - _player.transform.position).magnitude > _maxBrushDistance)
            Free();

        if (State == BrushState.Returning)
        {
            if ((_rigidbody2D.transform.position - _player.transform.position).magnitude < _minBrushDistance)
            {
                PutBack();
            }
            else
            {
                float speed = 10;
                float maxDistance = speed * Time.deltaTime;
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
    }

    public void Stuck(PaintOutline paintOutline)
    {
        if (paintOutline == _lastTouchedPaintOutline) return;

        State = BrushState.Stuck;
        _rigidbody2D.velocity = Vector2.zero;
        _lastTouchedPaintOutline = paintOutline;
    }

    public void Free()
    {
        if (State != BrushState.Thrown && State != BrushState.Stuck) return;
        _rigidbody2D.velocity = Vector2.zero;

        State = BrushState.Returning;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor) return;

        PaintOutline paintOutline = other.GetComponent<PaintOutline>();
        if (paintOutline) return;

        Free();
    }
}
