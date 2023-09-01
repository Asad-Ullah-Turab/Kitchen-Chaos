using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FryRecipeSO", menuName = "ScriptableObjects/FryRecipeSO")]
public class FryRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int fryTimeMax;
}

