using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    private InputActions _inputActions;

    private float _timeInput = 0.3f;
    private float _lastInput = -Mathf.Infinity;   

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (Time.time < _lastInput + _timeInput) return;
        OnInteract?.Invoke(this, EventArgs.Empty);
        _lastInput = Time.time;
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector3 input = _inputActions.Player.Move.ReadValue<Vector3>(); 
        input = input.normalized;
        return input;
    }    
}
