using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _bubbleParticles;

    [SerializeField]
    private AudioClip _bubbleClip;

    private void Start()
    {
        _bubbleParticles.Stop();
    }

    public void EnterMaterial(MaterialType materialType)
    {
        if (materialType == MaterialType.Water)
        {
            AudioManager.Instance.PlayEnvironment(_bubbleClip);
            _bubbleParticles.Play();
        }
        else
        {
            AudioManager.Instance.StopEnvironment();
            _bubbleParticles.Stop();
        }
    }

    public void ExitMaterial(MaterialType materialType)
    {
        if (materialType == MaterialType.Water)
        {
            AudioManager.Instance.StopEnvironment();
            _bubbleParticles.Stop();
        }
    }
}
