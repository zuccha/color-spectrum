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

public class Brush : MonoBehaviour
{
    public BrushPaint Paint
    {
        get
        {
            return 0 <= _selectedBrushColorIndex && _selectedBrushColorIndex < _availableBrushColors.Count
                ? _availableBrushColors[_selectedBrushColorIndex]
                : BrushPaint.Brown;
        }
    }

    private Rigidbody2D _rigidbody;
    [SerializeField]
    private SpriteRenderer _brushTipspriteRenderer;

    private int _selectedBrushColorIndex = 0;
    private List<BrushPaint> _availableBrushColors = new List<BrushPaint> {
        BrushPaint.Blue,
        BrushPaint.Brown,
        BrushPaint.Red,
    };

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
        _rigidbody = GetComponent<Rigidbody2D>();
        //_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _brushTipspriteRenderer.color = ColorByPaint(Paint);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void SwitchBrushColorLeft()
    {
        --_selectedBrushColorIndex;
        if (_selectedBrushColorIndex < 0) _selectedBrushColorIndex = _availableBrushColors.Count - 1;
        _brushTipspriteRenderer.color = ColorByPaint(Paint);
    }

    public void SwitchBrushColorRight()
    {
        _selectedBrushColorIndex = (_selectedBrushColorIndex + 1) % _availableBrushColors.Count;
        _brushTipspriteRenderer.color = ColorByPaint(Paint);
    }

    public void AddPaint(BrushPaint paint, bool autoSelect)
    {
        if (_availableBrushColors.Contains(paint)) return;
        _availableBrushColors.Add(paint);
        if (autoSelect)
        {
            _selectedBrushColorIndex = _availableBrushColors.Count - 1;
            _brushTipspriteRenderer.color = ColorByPaint(Paint);
        }
    }
}
