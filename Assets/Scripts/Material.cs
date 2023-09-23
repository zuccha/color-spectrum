using System.Collections.Generic;
using UnityEngine;

public enum MaterialType { None, Dirt, Lava, Water }

public class Material
{
  public MaterialType Type;
  public Color Color;
  public bool IsSolid = false;
  public bool ProducesHeat = false;

  public Material(MaterialType type, Color color, bool isSolid, bool producesHeat)
  {
    Type = type;
    Color = color;
    IsSolid = isSolid;
    ProducesHeat = producesHeat;
  }
}
