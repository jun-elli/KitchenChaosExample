using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredientsList;
    private List<KitchenObjectSO> ingredientsOnPlateList;

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
                return true;
            }
        }
        return false;
    }
}
