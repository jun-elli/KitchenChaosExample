using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    private int cuttingProgress;

    // Events
    public event Action OnCut;
    public event Action<float> OnProgressChanged;
    public static event Action<CuttingCounter> OnAnyCut; // If multiple counters, on any that is cutting
    public static new void ResetStaticData()
    {
        OnAnyCut = null;
    }

    // Methods
    public override void Interact(Player player)
    {

        // If there's no object in counter
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithRawObject(player.GetKitchenObject()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
            }
        }
        // If there's object on counter
        else
        {
            if (player.HasKitchenObject())
            {
                // If player has Plate, put ingredient in player's plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else // Player gets the object
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithRawObject(GetKitchenObject()))
        {
            KitchenObjectSO objectSO = GetKitchenObject().GetKitchenObjectSO();
            cuttingProgress++;
            OnCut?.Invoke();
            OnAnyCut?.Invoke(this);

            // If there's a recipe with max progress, update UI bar
            int maxProgress = GetMaxProgressFromRecipe(objectSO);
            if (maxProgress > 0)
            {
                OnProgressChanged?.Invoke(GetNormalizedProgress(maxProgress));
            }
            // If progress is at max, replace item
            if (cuttingProgress >= maxProgress)
            {
                // Find cut object output
                KitchenObjectSO cutObjectSO = FindCutObjectFromRawObject(objectSO);
                // Destroy object 
                GetKitchenObject().DestroySelf();
                // Instantiate sliced object
                KitchenObject.SpawnKitchenObject(cutObjectSO, this);
            }
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

    private bool HasRecipeWithRawObject(KitchenObject rawObject)
    {
        KitchenObjectSO rawObjectSO = rawObject.GetKitchenObjectSO();
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == rawObjectSO)
            {
                return true;
            }
        }
        return false;
    }
    private int GetMaxProgressFromRecipe(KitchenObjectSO rawObjectSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == rawObjectSO)
            {
                return recipe.cuttingProgressMax;
            }
        }
        return -1;
    }
    private float GetNormalizedProgress(int maxProgress)
    {
        return (float)cuttingProgress / maxProgress;
    }
}

