using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSo;
    public override void Interact(Player player)
    {
        if (HasKitchenObject)
        {
            if (!player.HasKitchenObject)
            {
                KitchenObject.KitchenObjectParent = player;
            }
        }
        else
        {
            if (player.HasKitchenObject)
            {
                player.KitchenObject.KitchenObjectParent = this;
            }
        }
    }
    public override void AltInteract(Player player)
    {
        if (HasKitchenObject)
        {
            KitchenObject.DestroySelf();

            KitchenObject = Instantiate(cutKitchenObjectSo.prefab, KitchenObjectFollowPoint.transform.position, Quaternion.identity, KitchenObjectFollowPoint.transform).GetComponent<KitchenObject>();
            KitchenObject.KitchenObjectParent = this;
        }
    }
}
