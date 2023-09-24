using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushIndicator : MonoBehaviour
{
    [SerializeField]
    private Image _brushImage;

    public Color TintColor { get; set; }

    public void SetTintColor(Color color)
    {
        _brushImage.color = color;
    }
}
