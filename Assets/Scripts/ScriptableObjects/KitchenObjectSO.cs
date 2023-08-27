using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectSO", menuName = "ScriptableObjects/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public string objectName;
}
