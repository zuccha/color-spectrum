using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintOutline : MonoBehaviour
{
    public Material Material;
    public GameObject Heat;

    BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;

    HashSet<Actor> _actors = new HashSet<Actor>();

    private static Material Dirt = new Material(MaterialType.Dirt, new Color(0.5f, 1f, 0f), true, false);
    private static Material Empty = new Material(MaterialType.None, new Color(1, 1, 1, 0.2f), false, false);
    private static Material Lava = new Material(MaterialType.Lava, new Color(1, 0, 0, 0.5f), false, true);
    private static Material Water = new Material(MaterialType.Water, new Color(0, 0, 1, 0.3f), false, false);

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Material = Empty;
        ApplyMaterial();
    }

    private void ApplyMaterial()
    {
        _boxCollider.isTrigger = !Material.IsSolid;
        _spriteRenderer.color = Material.Color;
        Heat.SetActive(Material.ProducesHeat);
        foreach (var actor in _actors)
            actor.SetMaterial(Material.Type);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor)
        {
            _actors.Add(actor);
            actor.SetMaterial(Material.Type);
        }

        Brush brush = other.GetComponent<Brush>();
        if (brush)
        {
            switch (brush.Paint)
            {
                case BrushPaint.Blue: Material = Water; break;
                case BrushPaint.Brown: Material = Dirt; break;
                case BrushPaint.Red: Material = Lava; break;
            }
            ApplyMaterial();
            brush.Stop();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor)
        {
            _actors.Remove(actor);
            actor.SetMaterial(MaterialType.None);
        }

        Brush brush = other.GetComponent<Brush>();
        if (brush)
        {
            Material = Empty;
            ApplyMaterial();
        }
    }
}
