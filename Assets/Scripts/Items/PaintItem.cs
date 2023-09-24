using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintItem : MonoBehaviour
{
    public BrushPaint Paint;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _collectSplashClip;

    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Brush.ColorByPaint(Paint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.AddBrushPaint(Paint);
            AudioManager.Instance.PlayEffect(_collectSplashClip, 1.1f);
            gameObject.SetActive(false);
        }
    }
}
