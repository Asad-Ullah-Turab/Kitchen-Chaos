using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public event EventHandler ObjectSpawnedEvent;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject)
        {
            KitchenObject = Instantiate(kitchenObjectSO.prefab, KitchenObjectFollowPoint.transform.position, Quaternion.identity, KitchenObjectFollowPoint.transform).GetComponent<KitchenObject>();
            KitchenObject.KitchenObjectParent = player;

            ObjectSpawnedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
