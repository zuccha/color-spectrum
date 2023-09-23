using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushPaint
{
    Blue,
    Brown,
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
    private SpriteRenderer _spriteRenderer;

    private int _selectedBrushColorIndex = 0;
    private List<BrushPaint> _availableBrushColors = new List<BrushPaint> {
        BrushPaint.Blue,
        BrushPaint.Brown,
        BrushPaint.Red,
    };

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ApplyColor();
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void SwitchBrushColorLeft()
    {
        _selectedBrushColorIndex = (_selectedBrushColorIndex + 1) % _availableBrushColors.Count;
        ApplyColor();
    }

    public void SwitchBrushColorRight()
    {
        --_selectedBrushColorIndex;
        if (_selectedBrushColorIndex < 0) _selectedBrushColorIndex = _availableBrushColors.Count - 1;
        ApplyColor();
    }

    private void ApplyColor()
    {
        switch (Paint)
        {
            case BrushPaint.Blue: _spriteRenderer.color = Color.blue; break;
            case BrushPaint.Brown: _spriteRenderer.color = Color.yellow; break;
            case BrushPaint.Red: _spriteRenderer.color = Color.red; break;
        }
    }
}
