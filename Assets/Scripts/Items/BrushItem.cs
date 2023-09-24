using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.SetHasBrush(true);
            gameObject.SetActive(false);
        }
    }
}
