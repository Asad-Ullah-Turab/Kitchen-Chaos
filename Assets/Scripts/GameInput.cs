using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
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
