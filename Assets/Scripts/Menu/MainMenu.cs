using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private MenuInputReader _menuInputReader;

    [SerializeField]
    private MainMenuItem _itemPlay;

    [SerializeField]
    private MainMenuItem _itemQuit;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _switchClip;
    [SerializeField]
    private AudioClip _confirmClip;

    private void Start()
    {
        _itemPlay.Select();
    }

    private void OnEnable()
    {
        _menuInputReader.Confirm += OnConfirm;
        _menuInputReader.MoveDown += OnMoveDown;
        _menuInputReader.MoveUp += OnMoveUp;
    }

    private void OnDisable()
    {
        _menuInputReader.Confirm -= OnConfirm;
        _menuInputReader.MoveDown -= OnMoveDown;
        _menuInputReader.MoveUp -= OnMoveUp;
    }

    private void OnConfirm()
    {
        AudioManager.Instance.PlayEffect(_confirmClip);
        if (_itemPlay.IsSelected) _itemPlay.Activate();
        if (_itemQuit.IsSelected) _itemQuit.Activate();
    }

    private void OnMoveDown()
    {
        AudioManager.Instance.PlayEffect(_switchClip);
        if (_itemPlay.IsSelected)
        {
            _itemPlay.Deselect();
            _itemQuit.Select();
        }
        else
        {
            _itemPlay.Select();
            _itemQuit.Deselect();
        }
    }

    private void OnMoveUp()
    {
        AudioManager.Instance.PlayEffect(_switchClip);
        if (_itemPlay.IsSelected)
        {
            _itemPlay.Deselect();
            _itemQuit.Select();
        }
        else
        {
            _itemPlay.Select();
            _itemQuit.Deselect();
        }
    }
}
