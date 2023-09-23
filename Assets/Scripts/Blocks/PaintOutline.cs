using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaintOutline : MonoBehaviour
{
    public Material Material;
    public GameObject Heat;

    BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;

    HashSet<Actor> _actors = new HashSet<Actor>();

    private static Material Dirt = new Material(MaterialType.Dirt, new Color(1, 1, 0), true, false);
    private static Material Empty = new Material(MaterialType.None, new Color(1, 1, 1), false, false);
    private static Material Lava = new Material(MaterialType.Lava, new Color(1, 0, 0), false, true);
    private static Material Water = new Material(MaterialType.Water, new Color(0, 0, 1), false, false);

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Material = Empty;
        ApplyMaterial();

        Debug.Log(_spriteRenderer.size);
        BoxCollider2D heatBoxCollider = Heat.GetComponent<BoxCollider2D>();
        SpriteRenderer heatSpriteRenderer = Heat.GetComponent<SpriteRenderer>();
        heatBoxCollider.size = new Vector2(_spriteRenderer.size.x, heatBoxCollider.size.y);
        heatSpriteRenderer.size = heatBoxCollider.size;
        Debug.Log(heatSpriteRenderer);
        Heat.transform.localPosition = new Vector3(0.0f, _spriteRenderer.size.y / 2 + heatBoxCollider.size.y / 2, Heat.transform.localPosition.z);

    }

    private void ApplyMaterial()
    {
        _boxCollider.isTrigger = !Material.IsSolid;
        gameObject.layer = LayerMask.NameToLayer(Material.IsSolid ? "Ground" : "Default");
        _spriteRenderer.color = Material.Color;
        if (Heat != null)
        {
            Heat.SetActive(Material.ProducesHeat);
        }
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
