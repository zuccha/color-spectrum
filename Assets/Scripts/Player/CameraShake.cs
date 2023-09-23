using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private float _shakeTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _perlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        _perlin.m_AmplitudeGain = intensity;
        _shakeTimer = duration;
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                _perlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
