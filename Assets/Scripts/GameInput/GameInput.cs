using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction; 

    private InputActions _inputActions;
    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);    
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector3 input = _inputActions.Player.Move.ReadValue<Vector3>(); 
        input = input.normalized;
        return input;
    }    
}
