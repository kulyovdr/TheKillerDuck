using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
    public bool IsAttacking()
    {
        return _playerInputActions.Comdat.Attack.triggered;
    }

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
