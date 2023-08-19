using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{

    // movement support
    [SerializeField] private float movementSpeed = 5f;
    private bool isWalking;

    // input support
    [SerializeField] private GameInput gameInput;

    /// <summary>
    /// Gets whether or not the player is walking.
    /// </summary>
    public bool IsWalking
    {
        get { return isWalking; }
    }

    /// <summary>
    /// Gets called every frame.
    /// </summary>
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // move the player
        Vector3 dirVector = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += dirVector * movementSpeed * Time.deltaTime;

        // set IsWalking to true if the player is moving
        isWalking = inputVector != Vector2.zero;

        // Rotate the player smoothly to face the direction of movement
        float rotationSpeed = 5f;
        if (dirVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirVector), Time.deltaTime * rotationSpeed);
        }
        
    }

}
