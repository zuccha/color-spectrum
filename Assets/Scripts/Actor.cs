using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
  public MaterialType MaterialType = MaterialType.None;

  private Rigidbody2D _rigidbody;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    switch (MaterialType)
    {
      case MaterialType.Dirt:
      case MaterialType.Lava:
        // TODO: Kill player.
        break;
      case MaterialType.Water:
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, 0.2f);
        break;
    }
  }
}
