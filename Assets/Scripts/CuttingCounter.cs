using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    public override void Interact(Player player)
    {

        // If there's no object in counter
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithRawObject(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        // If there's object on counter
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithRawObject(GetKitchenObject().GetKitchenObjectSO()))
        {
            // Find cut object output
            KitchenObjectSO cutObjectSO = FindCutObjectFromRawObject(GetKitchenObject().GetKitchenObjectSO());
            // Destroy object 
            GetKitchenObject().DestroySelf();
            // Instantiate sliced object
            KitchenObject.SpawnKitchenObject(cutObjectSO, this);
        }
    }

    private KitchenObjectSO FindCutObjectFromRawObject(KitchenObjectSO rawObjectSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == rawObjectSO)
            {
                return recipe.output;
            }
        }
        return null;
    }

    private bool HasRecipeWithRawObject(KitchenObjectSO rawObjectSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == rawObjectSO)
            {
                return true;
            }
        }
        return false;
    }
}

