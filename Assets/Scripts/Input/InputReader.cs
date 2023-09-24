using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Core/Input Reader", fileName = "InputReader")]
public class InputReader : ScriptableObject, PlayerControls.IGameplayActions
{
    private PlayerControls _playerControls;

    public event Action<float> MovePerformed;
    public event Action MoveCanceled;

    public event Action JumpPerformed;

    public event Action ThrowBrushPerformed;

    public event Action SwitchBrushColorLeft;
    public event Action SwitchBrushColorRight;

    public event Action PausePerformed;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.Gameplay.SetCallbacks(this);
        }
        _playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Gameplay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                MovePerformed?.Invoke(context.ReadValue<float>());
                break;
            case InputActionPhase.Canceled:
                MoveCanceled?.Invoke();
                break;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                JumpPerformed?.Invoke();
                break;
        }
    }

    public void OnThrowBrush(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                ThrowBrushPerformed?.Invoke();
                break;
        }
    }

    public void OnSwitchBrushColorLeft(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                SwitchBrushColorLeft?.Invoke();
                break;
        }
    }

    public void OnSwitchBrushColorRight(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                SwitchBrushColorRight?.Invoke();
                break;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                PausePerformed?.Invoke();
                break;
        }
    }
}
