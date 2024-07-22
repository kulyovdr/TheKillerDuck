using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnPlayerAttack;

    private void Awake()
    {
        Instance = this;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Comdat.Attack.started += PlayerAttack_started;

    }

    public Vector2 GetMovementVectorKeyboard()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

   /* public Vector2 GetMovementVectorJoystick()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }*/


    public Vector3 GetMousePosition()
    {
        Vector3 mousePosit = Mouse.current.position.ReadValue();
        return mousePosit;
    }

    private void PlayerAttack_started(InputAction.CallbackContext odj)
    {
        if (OnPlayerAttack != null)
        {
            OnPlayerAttack.Invoke(this, EventArgs.Empty);
        }
    }
}
