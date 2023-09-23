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
  private static Material Empty = new Material(MaterialType.None, new Color(0.9f, 0.9f, 0.9f), false, false);
  private static Material Lava = new Material(MaterialType.Lava, Color.red, false, true);
  private static Material Water = new Material(MaterialType.Water, Color.blue, false, false);

  void Start()
  {
    _boxCollider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    Material = Empty;
    ApplyMaterial();
  }

  void Update()
  {
    if (Input.GetKey(KeyCode.W)) Material = Water;
    if (Input.GetKey(KeyCode.D)) Material = Dirt;
    if (Input.GetKey(KeyCode.L)) Material = Lava;
    if (Input.GetKey(KeyCode.Backspace)) Material = Empty;
    ApplyMaterial();
  }

  private void ApplyMaterial()
  {
    _boxCollider.isTrigger = !Material.IsSolid;
    _spriteRenderer.color = Material.Color;
    Heat.SetActive(Material.ProducesHeat);
    foreach (var actor in _actors)
      actor.MaterialType = Material.Type;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Actor actor = other.GetComponent<Actor>();
    if (actor)
    {
      _actors.Add(actor);
      actor.MaterialType = Material.Type;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    Actor actor = other.GetComponent<Actor>();
    if (actor)
    {
      _actors.Remove(actor);
      actor.MaterialType = MaterialType.None;
    }
  }
}
