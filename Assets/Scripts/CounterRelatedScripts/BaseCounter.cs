using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private GameObject kitchenObjectFollowPoint;
    private KitchenObject kitchenObject;

    public GameObject KitchenObjectFollowPoint { get { return kitchenObjectFollowPoint; } }
    public KitchenObject KitchenObject
    {
        get { return kitchenObject; }
        set { kitchenObject = value; }
    }
    public bool HasKitchenObject
    {
        get { return kitchenObject != null; }
    }

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact() called!");
    }
    public virtual void AltInteract(Player player)
    {
        //Debug.LogError("BaseCounter.Interact() called!");
    }
}
