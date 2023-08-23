using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public bool HasKitchenObject { get; }
    public KitchenObject KitchenObject { get; set; }
    public GameObject KitchenObjectFollowPoint { get; }
}
