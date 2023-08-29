using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CutRecipeSO[] cutRecipeSoArray;

    public class OnCuttingProgressChangedEventArgs : EventArgs
    {
        public float progressNormalzied;
    }
    public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged; // for UI

    public event EventHandler OnCut; // for animation

    private int cuttingProgressCounter;
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
                cuttingProgressCounter = 0;
                OnCuttingProgressChanged?.Invoke(this,
                    new OnCuttingProgressChangedEventArgs
                    {
                        progressNormalzied = cuttingProgressCounter / (float)GetCutRecipeForInput(KitchenObject.KitchenObjectSO).cuttingCounterMax
                    });
            }
        }
    }
    public override void AltInteract(Player player)
    {
        if (HasKitchenObject && HasRecipeWithInput(KitchenObject.KitchenObjectSO))
        {
            Slice();
            if (cuttingProgressCounter >= GetCutRecipeForInput(KitchenObject.KitchenObjectSO).cuttingCounterMax)
            {
                CutKitchenObject();
            }
        }   
    }

    private void CutKitchenObject()
    {
        KitchenObjectSO output = GetOutputFromInput(KitchenObject.KitchenObjectSO);
        KitchenObject.DestroySelf();
        KitchenObject.SpawnKitchenObject(output, this);
    }
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        CutRecipeSO cutRecipe = GetCutRecipeForInput(input);
        if (cutRecipe != null)
        {
            return cutRecipe.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        CutRecipeSO cutRecipe = GetCutRecipeForInput(input);
        return cutRecipe != null;
    }

    private CutRecipeSO GetCutRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CutRecipeSO cutRecipeSO in cutRecipeSoArray)
        {
            if (cutRecipeSO.input == inputKitchenObjectSO)
            {
                return cutRecipeSO;
            }
        }
        return null;
    }

    private void Slice()
    {
        cuttingProgressCounter++;
        OnCut?.Invoke(this, EventArgs.Empty);
        OnCuttingProgressChanged?.Invoke(this,
                new OnCuttingProgressChangedEventArgs
                {
                    progressNormalzied = cuttingProgressCounter / (float)GetCutRecipeForInput(KitchenObject.KitchenObjectSO).cuttingCounterMax
                });
    }
}