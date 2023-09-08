using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    private PlayerInputActions _inputActions;

    public event EventHandler OnPlayerJumpInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnPlayerJumpInput?.Invoke(this, EventArgs.Empty);
    }

    public float GetPlayerMovement()
    {
        return _inputActions.Player.Movement.ReadValue<float>();
    }
}
