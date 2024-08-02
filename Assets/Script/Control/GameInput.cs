using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GameInput : MonoBehaviour
{
    [SerializeField] public Joystick joystickMove;
    [SerializeField] public Joystick joystickAttack;

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

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosit = Mouse.current.position.ReadValue();
        return mousePosit;
    }

    //public Vector2 GetJoystickPosition()
   // {
        //Vector2 joystickInput = joystickAttack.position;
       // return joystickPosition;
   // }

    private void PlayerAttack_started(InputAction.CallbackContext odj)
    {
        if (OnPlayerAttack != null)
        {
            OnPlayerAttack.Invoke(this, EventArgs.Empty);
        }
    }
}
