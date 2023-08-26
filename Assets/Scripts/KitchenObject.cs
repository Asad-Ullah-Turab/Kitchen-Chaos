using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public void DestroySelf()
    {
        ClearParent();
        Destroy(gameObject);
    }

    private void ClearParent()
    {
        kitchenObjectParent = null;
    }

    //public void Swap(KitchenObject otherKitchenObject)
    //{
    //    // Swap the kitchen object with the other kitchen object

    //    // swap parents
    //    otherKitchenObject.transform.SetParent(this.kitchenObjectParent.KitchenObjectFollowPoint.transform);
    //    otherKitchenObject.transform.localPosition = Vector3.zero;

    //    transform.SetParent(otherKitchenObject.kitchenObjectParent.KitchenObjectFollowPoint.transform);
    //    transform.localPosition = Vector3.zero;

    //    // correct data
    //    otherKitchenObject.kitchenObjectParent = this.kitchenObjectParent;
    //    otherKitchenObject.kitchenObjectParent.KitchenObject = this;

    //    kitchenObjectParent = otherKitchenObject.kitchenObjectParent;
    //    kitchenObjectParent.KitchenObject = this;
    //}
}
