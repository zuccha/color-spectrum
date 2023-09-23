using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatable : MonoBehaviour
{
    protected bool _isHeating = false;

    private bool _nextIsHeating;
    private float _cooldown = 0;

    public void SetIsHeating(bool isHeating)
    {
        if (isHeating)
        {
            _isHeating = true;
        }
        else
        {
            _nextIsHeating = isHeating;
            _cooldown = 0.2f;

        }
    }

    protected virtual void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0)
            {
                _cooldown = 0;
                _isHeating = _nextIsHeating;
            }
        }
    }
}
