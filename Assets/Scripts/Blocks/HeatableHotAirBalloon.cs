using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloon : Heatable
{
  float _initialPositionY;
  Rigidbody2D _rigidbody;

  private void Start()
  {
    _initialPositionY = transform.position.y;
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    _rigidbody.velocity = IsHeating ? Vector2.up : Vector2.down;
    transform.position = new Vector3(
      transform.position.x,
      Mathf.Max(transform.position.y, _initialPositionY),
      transform.position.z);
  }
}
