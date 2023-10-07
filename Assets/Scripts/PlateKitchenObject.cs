using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredientsList;
    private List<KitchenObjectSO> ingredientsOnPlateList;

    // Events
    public event Action<KitchenObjectSO> OnIngredientAdded;

    private void Awake()
    {
        ingredientsOnPlateList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO objectSO)
    {
        if (validIngredientsList.Contains(objectSO))
        {
            if (!ingredientsOnPlateList.Contains(objectSO))
            {
                ingredientsOnPlateList.Add(objectSO);
                OnIngredientAdded?.Invoke(objectSO);
                return true;
            }
        }
        return false;
    }
}
