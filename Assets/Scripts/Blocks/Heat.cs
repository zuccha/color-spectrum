using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    Heatable heatable = other.GetComponent<Heatable>();
    if (heatable) heatable.IsHeating = true;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    Heatable heatable = other.GetComponent<Heatable>();
    if (heatable) heatable.IsHeating = false;
  }
}
