using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Core/Menu Input Reader", fileName = "MenuInputReader")]
public class MenuInputReader : ScriptableObject, PlayerControls.IMenuActions
{
    private PlayerControls _playerControls;

    public event Action Confirm;
    public event Action MoveDown;
    public event Action MoveUp;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.Menu.SetCallbacks(this);
        }
        _playerControls.Menu.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Menu.Disable();
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Confirm?.Invoke();
                break;
        }
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                MoveUp?.Invoke();
                break;
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                MoveDown?.Invoke();
                break;
        }
    }
}
