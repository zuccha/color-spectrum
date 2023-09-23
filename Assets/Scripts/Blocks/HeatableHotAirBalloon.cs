using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloon : Heatable
{
    private Rigidbody2D _rigidbody;
    private float _initialPositionY;

    private void Start()
    {
        _initialPositionY = transform.position.y;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        _rigidbody.velocity = _isHeating ? Vector2.up * 0.5f : Vector2.down * 0.5f;
        transform.position = new Vector3(
          transform.position.x,
          Mathf.Max(transform.position.y, _initialPositionY),
          transform.position.z);
    }
}
