using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryRecipeSO[] fryRecipeSoArray;

    private FryRecipeSO fryRecipe;

    private float fryTimer;

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
            if (player.HasKitchenObject && HasRecipeWithInput(player.KitchenObject.KitchenObjectSO))
            {
                player.KitchenObject.KitchenObjectParent = this;
                fryRecipe = GetFryRecipeForInput(KitchenObject.KitchenObjectSO);
                fryTimer = 0f;
            }
        }
    }

    private void Update()
    {
        if (HasKitchenObject && HasRecipeWithInput(KitchenObject.KitchenObjectSO))
        {
            Fry();
        }
    }
    private void Fry()
    {
        fryTimer+= Time.deltaTime;

        Debug.Log(fryTimer);
        if(fryTimer >= fryRecipe.fryTimeMax)
        {
            KitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(fryRecipe.output, this);
        }
    }
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        FryRecipeSO fryRecipe = GetFryRecipeForInput(input);
        if (fryRecipe != null)
        {
            return fryRecipe.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        FryRecipeSO fryRecipe = GetFryRecipeForInput(input);
        return fryRecipe != null;
    }

    private FryRecipeSO GetFryRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryRecipeSO fryRecipeSO in fryRecipeSoArray)
        {
            if (fryRecipeSO.input == inputKitchenObjectSO)
            {
                return fryRecipeSO;
            }
        }
        return null;
    }

}
