using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatableHotAirBalloon : Heatable
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _initialPositionY;

    [SerializeField]
    private float _floatSpeed = 0.5f;

    private void Start()
    {
        _initialPositionY = transform.position.y;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        _rigidbody.velocity = _isHeating ? Vector2.up * _floatSpeed : Vector2.down * _floatSpeed * 1.5f;
        transform.position = new Vector3(
          transform.position.x,
          Mathf.Max(transform.position.y, _initialPositionY),
          transform.position.z);

        _animator.Play("HotAirBalloonHeat", -1, _heat / MAX_HEAT);
    }
}
