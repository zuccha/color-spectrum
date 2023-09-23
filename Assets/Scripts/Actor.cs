using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float MaxLavaDamage = 2f;

    protected Vector2 _maxVelocity;
    protected MaterialType _materialType = MaterialType.None;

    private float _damage = 0;
    private Rigidbody2D _rigidbody;

    public virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    public void SetMaterial(MaterialType materialType)
    {
        _materialType = materialType;
        _damage = 0;
    }

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        switch (_materialType)
        {
            case MaterialType.Dirt:
                Kill();
                break;
            case MaterialType.Lava:
                _damage += Time.deltaTime;
                _rigidbody.velocity = new Vector2(
                    _rigidbody.velocity.x,
                    Mathf.Max(_rigidbody.velocity.y, -0.5f)
                );
                break;
            case MaterialType.Water:
                _rigidbody.velocity = new Vector2(
                    _rigidbody.velocity.x,
                    Mathf.Max(_rigidbody.velocity.y, -1f)
                );
                break;
        }
        if (_damage >= MaxLavaDamage) Kill();
    }

    protected virtual void FixedUpdate()
    {

    }
}
