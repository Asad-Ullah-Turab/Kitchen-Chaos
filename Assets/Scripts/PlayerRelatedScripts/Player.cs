using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Fields
    // movement support
    [SerializeField] private float movementSpeed = 5f;
    private bool isWalking;

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
        // input support
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }

        // normalize input vector so diagonal movement isn't faster
        inputVector.Normalize();

        // move the player
        Vector3 dirVector = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += dirVector * movementSpeed * Time.deltaTime;

        // set IsWalking to true if the player is moving
        isWalking = inputVector != Vector2.zero;

        // Rotate the player smoothly to face the direction of movement
        float rotationSpeed = 3.5f;
        if (dirVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirVector), 0.15F * rotationSpeed);
        }
        
    }

    #endregion
}
