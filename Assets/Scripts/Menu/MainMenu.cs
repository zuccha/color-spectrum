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
        if (_itemPlay.IsSelected) _itemPlay.Activate();
        if (_itemQuit.IsSelected) _itemQuit.Activate();
    }

    private void OnMoveDown()
    {
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
