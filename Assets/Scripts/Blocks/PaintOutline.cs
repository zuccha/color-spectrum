using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaintOutline : MonoBehaviour
{
    public Material Material;
    public GameObject Heat;
    public ParticleSystem HeatParticles;
    public float HeatHeight = 6;

    BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    private SpriteRenderer _fillColorSpriteRenderer;

    HashSet<Actor> _actors = new HashSet<Actor>();
    HashSet<Mouth> _mouths = new HashSet<Mouth>();

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

        BoxCollider2D heatBoxCollider = Heat.GetComponent<BoxCollider2D>();
        heatBoxCollider.size = new Vector2(_spriteRenderer.size.x, HeatHeight);
        HeatParticles.Stop();
        ParticleSystem.ShapeModule heatParticlesShapeModule = HeatParticles.shape;
        heatParticlesShapeModule.radius = _spriteRenderer.size.x / 2;
        Heat.transform.localPosition = new Vector3(0.0f, _spriteRenderer.size.y / 2 + HeatHeight / 2, Heat.transform.localPosition.z);
        _fillColorSpriteRenderer.size = _spriteRenderer.size;
        _fillColorSpriteRenderer.enabled = false;
    }

    private void ApplyMaterial()
    {
        _boxCollider.isTrigger = !Material.IsSolid;
        gameObject.layer = LayerMask.NameToLayer(Material.IsSolid ? "Ground" : "Default");
        _fillColorSpriteRenderer.color = Material.Color;
        if (Heat != null)
        {
            Heat.SetActive(Material.ProducesHeat);
            if (Material.ProducesHeat) HeatParticles.Play();
            else HeatParticles.Stop();
        }
        foreach (var actor in _actors)
            actor.SetMaterial(Material.Type);
        foreach (var mouth in _mouths)
            mouth.EnterMaterial(Material.Type);
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
            brush.Stuck(this);
            _fillColorSpriteRenderer.enabled = true;
        }

        Mouth mouth = other.GetComponent<Mouth>();
        if (mouth)
        {
            _mouths.Add(mouth);
            mouth.EnterMaterial(Material.Type);
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
            brush.Free();
            _fillColorSpriteRenderer.enabled = false;
        }

        Mouth mouth = other.GetComponent<Mouth>();
        if (mouth)
        {
            _mouths.Remove(mouth);
            mouth.ExitMaterial(Material.Type);
        }
    }
}
