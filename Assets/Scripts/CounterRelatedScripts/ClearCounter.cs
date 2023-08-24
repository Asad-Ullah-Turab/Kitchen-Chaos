using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
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
} 