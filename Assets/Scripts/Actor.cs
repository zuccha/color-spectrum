using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected Vector2 _maxVelocity;
    protected MaterialType _materialType = MaterialType.None;

    private Rigidbody2D _rigidbody;

    [Header("UI-Elements")]
    [SerializeField]
    private Healthbar _healthbar;

    protected float _maxHealth = 100f;
    protected float _currentHealth;

    public virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    public void SetMaterial(MaterialType materialType)
    {
        _materialType = materialType;
    }

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;

        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    protected virtual void Update()
    {
        switch (_materialType)
        {
            case MaterialType.Dirt:
                Kill();
                break;
            case MaterialType.Lava:
                _currentHealth -= 1f;
                _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
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
        if (_currentHealth <= 0f) Kill();
    }

    protected virtual void FixedUpdate()
    {

    }
}
