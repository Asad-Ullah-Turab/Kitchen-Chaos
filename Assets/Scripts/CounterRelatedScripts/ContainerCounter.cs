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
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // For animation
            ObjectSpawnedEvent(this, EventArgs.Empty);
        }
    }
}
