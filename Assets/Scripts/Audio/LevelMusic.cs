using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip _levelMusicClip;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_levelMusicClip);
    }
}
