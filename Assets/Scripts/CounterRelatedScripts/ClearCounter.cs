using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
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

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            kitchenObject = Instantiate(kitchenObjectSO.prefab, kitchenObjectFollowPoint.transform.position, Quaternion.identity, kitchenObjectFollowPoint.transform).GetComponent<KitchenObject>();
            kitchenObject.KitchenObjectParent = this;    
        }
        else
        {
            kitchenObject.KitchenObjectParent = player;
        }
    }
} 