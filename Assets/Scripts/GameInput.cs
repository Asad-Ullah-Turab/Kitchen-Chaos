using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    public event EventHandler OnInteract;
    public event EventHandler OnAltInteract;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.AltInteract.performed += AltInteract_performed;
    }

    private void AltInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAltInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        // input support
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // normalize input vector so diagonal movement isn't faster
        inputVector.Normalize();

        return inputVector;
    }
}
