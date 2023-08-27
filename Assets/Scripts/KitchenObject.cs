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
            if (value.HasKitchenObject)
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
        kitchenObjectParent.KitchenObject = null;
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        KitchenObject kitchenObject = Instantiate(kitchenObjectSO.prefab, kitchenObjectParent.KitchenObjectFollowPoint.transform.position, Quaternion.identity).GetComponent<KitchenObject>();
        kitchenObject.KitchenObjectParent = kitchenObjectParent;

        return kitchenObject;
    }
}
