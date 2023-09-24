using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private Button _resumeButton;
    [SerializeField]
    private Button _backToMainMenuButton;

    private void Start()
    {
        Hide();

        _inputReader.PausePerformed += OnPausePerformed;

        _resumeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        _backToMainMenuButton.onClick.AddListener(() =>
        {
            Hide();
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void OnPausePerformed()
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        _inputReader.PausePerformed -= OnPausePerformed;
    }
}
