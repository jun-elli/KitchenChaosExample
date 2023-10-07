using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validIngredientsList;
    private List<KitchenObjectSO> _ingredientsOnPlateList;

    // Events
    public event Action<KitchenObjectSO> OnIngredientAdded;

    private void Awake()
    {
        _ingredientsOnPlateList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO objectSO)
    {
        if (_validIngredientsList.Contains(objectSO))
        {
            if (!_ingredientsOnPlateList.Contains(objectSO))
            {
                _ingredientsOnPlateList.Add(objectSO);
                OnIngredientAdded?.Invoke(objectSO);
                return true;
            }
        }
        return false;
    }

    public List<KitchenObjectSO> GetIngredientsOnPlate()
    {
        return _ingredientsOnPlateList;
    }
}
