using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioClip _menuMusicClip;
    [SerializeField]
    private AudioClip _levelMusicClip;

    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _effectsSource;
    [SerializeField]
    private AudioSource _environmentSource;

    private String _currentSceneName;
    private String _previousSceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;

        if (_currentSceneName == "MainMenu")
        {
            PlayMusic(_menuMusicClip);
        }
        else
        {
            PlayMusic(_levelMusicClip);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _previousSceneName = _currentSceneName;
        _currentSceneName = scene.name;

        if (_previousSceneName == _currentSceneName) return;

        if (_currentSceneName == "MainMenu")
        {
            PlayMusic(_menuMusicClip);
        }

        if (_currentSceneName == "Level - 001")
        {
            PlayMusic(_levelMusicClip);
        }
    }

    public void PlayMusic(AudioClip audioClip)
    {
        _musicSource.clip = audioClip;
        _musicSource.Play();
    }

    public void PlayEffect(AudioClip audioClip, float volumeScale = 1f)
    {
        _effectsSource.PlayOneShot(audioClip, volumeScale);
    }

    public void PlayEnvironment(AudioClip audioClip, float volumeScale = 1f)
    {
        _environmentSource.clip = audioClip;
        _environmentSource.Play();
    }

    public void StopEnvironment()
    {
        _environmentSource.Stop();
    }
}
