using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatable : MonoBehaviour
{
    protected bool _isHeating = false;
    protected int _heat = 0;

    public static int MAX_HEAT = 1024;

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


        _heat = _isHeating
            ? Mathf.Min(_heat + 1, MAX_HEAT)
            : Mathf.Max(_heat - 1, 0);
    }
}
