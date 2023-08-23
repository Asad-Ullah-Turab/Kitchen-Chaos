using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public IKitchenObjectParent KitchenObjectParent
    {
        get { return kitchenObjectParent; }
        set 
        {
            if (value.HasKitchenObject && kitchenObjectParent != null)
            {
                Debug.LogError("Counter already has a kitchen object on it!");
            }

            if (kitchenObjectParent != null)
            {
                kitchenObjectParent.KitchenObject = null;
            }
            kitchenObjectParent = value;
            transform.SetParent(kitchenObjectParent.KitchenObjectFollowPoint.transform);
            transform.localPosition = Vector3.zero;
            kitchenObjectParent.KitchenObject = this;
        }
    }

    public KitchenObjectSO KitchenObjectSO 
    {
        get { return kitchenObjectSO; }
    }
}
