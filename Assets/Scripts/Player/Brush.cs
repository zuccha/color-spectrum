using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushColor
{
    Blue,
    Brown,
    Red,
}

public class Brush : MonoBehaviour
{
    public BrushColor Color;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}
