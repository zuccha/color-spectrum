using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
  public float MaxLavaDamage = 2f;

  private MaterialType _materialType = MaterialType.None;
  private float _damage = 0;

  private Rigidbody2D _rigidbody;

  public void SetMaterial(MaterialType materialType)
  {
    _materialType = materialType;
    _damage = 0;
  }

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    switch (_materialType)
    {
      case MaterialType.Dirt:
        gameObject.SetActive(false);
        break;
      case MaterialType.Lava:
        _damage += Time.deltaTime;
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, 0.2f);
        break;
      case MaterialType.Water:
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, 0.4f);
        break;
    }
    if (_damage >= MaxLavaDamage) gameObject.SetActive(false);
  }
}
