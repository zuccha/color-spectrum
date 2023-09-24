using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _bubbleParticles;

    private void Start()
    {
        _bubbleParticles.Stop();
    }

    public void EnterMaterial(MaterialType materialType)
    {
        if (materialType == MaterialType.Water)
            _bubbleParticles.Play();
        else
            _bubbleParticles.Stop();
    }

    public void ExitMaterial(MaterialType materialType)
    {
        if (materialType == MaterialType.Water)
            _bubbleParticles.Stop();
    }
}
