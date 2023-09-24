using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushIndicators : MonoBehaviour
{
    [SerializeField]
    private GameObject _brushIndicatorPrefab;
    [SerializeField]
    private Transform _horizontalContainer;
    [SerializeField]
    private Brush _brush;

    private void Awake()
    {
        _brush.BrushColorAdded += OnBrushColorAdded;

        foreach (BrushPaint _brushPaint in _brush.GetAvailableBrushColors())
        {
            GameObject _brushIndicator = Instantiate(_brushIndicatorPrefab, _horizontalContainer);
            _brushIndicator.GetComponent<BrushIndicator>().SetTintColor(Brush.ColorByPaint(_brushPaint));
        }
    }

    private void OnBrushColorAdded(Color _brushTipColor)
    {
        Debug.Log(_brushTipColor);
        GameObject _brushIndicator = Instantiate(_brushIndicatorPrefab, _horizontalContainer);
        _brushIndicator.GetComponent<BrushIndicator>().SetTintColor(_brushTipColor);
    }
}
