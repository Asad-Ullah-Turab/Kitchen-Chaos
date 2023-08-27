using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CutRecipeSO[] cutRecipeSoArray;
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
        if (HasKitchenObject && HasRecipeWithInput(KitchenObject.KitchenObjectSO))
        {
            KitchenObjectSO output = GetOutputFromInput(KitchenObject.KitchenObjectSO);
            KitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(output, this);
        }
    }
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    { 
        foreach (CutRecipeSO cutRecipeSO in cutRecipeSoArray)
        {
            if (cutRecipeSO.input == input)
            {
                return cutRecipeSO.output;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        foreach (CutRecipeSO cutRecipeSO in cutRecipeSoArray)
        {
            if (cutRecipeSO.input == input)
            {
                return true;
            }
        }
        return false;
    }
}