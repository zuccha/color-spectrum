using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushItem : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioClip _collectSplashClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.SetHasBrush(true);
            AudioManager.Instance.PlayEffect(_collectSplashClip, 1.1f);
            gameObject.SetActive(false);
        }
    }
}
