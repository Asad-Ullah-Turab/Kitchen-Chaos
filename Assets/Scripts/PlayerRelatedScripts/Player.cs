using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Fields

    // movement support
    [SerializeField] private float movementSpeed = 5f;
    private bool isWalking;
    private float playerHeight = 2.6f;
    private float playerRadius = 0.6f;

    // input support
    [SerializeField] private GameInput gameInput;

    // interaction support
    private Vector3 lastInteractDir;
    [SerializeField] private LayerMask counterLayerMask;

    #endregion

    #region Properties
    /// <summary>
    /// Gets whether or not the player is walking.
    /// </summary>
    public bool IsWalking
    {
        get { return isWalking; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets called every frame.
    /// </summary>
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    /// <summary>
    /// Handles player movement.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 dirVector = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = movementSpeed * Time.deltaTime;

        // collision check using raycast
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirVector, moveDistance);

        if (!canMove)
        {
            Vector3 dirVectorX = new Vector3(dirVector.x, 0, 0).normalized;
            Vector3 dirVectorZ = new Vector3(0, 0, dirVector.z).normalized;

            // check if the player can move in the x direction
            bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirVectorX, moveDistance);
            if (canMoveX)
            {
                dirVector = dirVectorX;
                canMove = true;
            }
            else
            {
                // check if the player can move in the z direction
                bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirVectorZ, moveDistance);
                if (canMoveZ)
                {
                    dirVector = dirVectorZ;
                    canMove = true;
                }
            }
        }

        // move the player
        if (canMove)
        {
            transform.position += dirVector * moveDistance;
        }


        // set IsWalking to true if the player is moving
        isWalking = inputVector != Vector2.zero;

        // Rotate the player smoothly to face the direction of movement
        float rotationSpeed = 5f;
        if (dirVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirVector), Time.deltaTime * rotationSpeed);
        }

    }
    
    /// <summary>
    /// Handles player interaction.
    /// </summary>
    private void HandleInteraction()
    {
        // get the input vector
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 dirVector = new Vector3(inputVector.x, 0, inputVector.y);

        // if the player is not moving, use the last interact direction
        if (dirVector != Vector3.zero)
        {
            lastInteractDir = dirVector;
        }

        float interactDistance = 2f;
        bool hitSomething = Physics.Raycast(transform.position + (Vector3.up * playerHeight / 4), lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask);
                            
        if(hitSomething)
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }

        
    } 

    #endregion
}
