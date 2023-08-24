using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    
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
    private BaseCounter selectedCounter;

    // Kitchen Object parent support
    private KitchenObject kitchenObject;
    [SerializeField] private GameObject kitchenObjectFollowPoint;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the player.
    /// </summary>
    public static Player Instance { get; private set; }

    /// <summary>
    /// Gets whether or not the player is walking.
    /// </summary>
    public bool IsWalking
    {
        get { return isWalking; }
    }

    public bool HasKitchenObject { get { return kitchenObject != null; } }

    public KitchenObject KitchenObject 
    {
        get { return kitchenObject; }
        set { kitchenObject = value; }
    }

    public GameObject KitchenObjectFollowPoint { get { return kitchenObjectFollowPoint; } }
    #endregion

    #region Methods

    private void Awake()
    {
        // Singleton support
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        gameInput.OnInteract += GameInput_OnInteract;
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    /// <summary>
    /// Gets called every frame.
    /// </summary>
    private void Update()
    {
        HandleMovement();
        HandleCounterSelection();
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
    private void HandleCounterSelection()
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter counter))
            {
                if (counter != selectedCounter)
                {
                    ChangeSelectedCounter(counter);
                }   
            }
            else
            {
                if (selectedCounter != null)
                {
                    ChangeSelectedCounter(null);
                }
            }
        }
        else
        {
            if (selectedCounter != null)
            {
                ChangeSelectedCounter(null);
            }
        }
    } 

    private void ChangeSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = this.selectedCounter });
    }

    #endregion
}
