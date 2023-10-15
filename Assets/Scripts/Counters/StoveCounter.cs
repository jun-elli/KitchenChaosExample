using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public enum StoveState
    {
        Idle,
        Frying,
        Fried,
        Burned
    }


    [SerializeField] private FryingRecipeSO[] recipes;

    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO currentRecipe;
    private StoveState state;

    public bool HasFriedState => state == StoveState.Fried;

    // Events
    public event Action<StoveState> OnStateChanged;
    public event Action<float> OnProgressChanged;


    private void Start()
    {
        state = StoveState.Idle;
    }
    private void Update()
    {
        HandleFryingTimer();
    }

    public override void Interact(Player player)
    {

        // If there's no object in counter
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithRawObject(player.GetKitchenObject()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                currentRecipe = GetFryingRecipe(GetKitchenObject());
                fryingTimer = 0f;
                burningTimer = 0f;
                OnProgressChanged?.Invoke(0);
                state = StoveState.Frying;
                OnStateChanged?.Invoke(state);
            }
        }
        // If there's an object on counter
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
                        state = StoveState.Idle;
                        OnStateChanged?.Invoke(state);
                        OnProgressChanged?.Invoke(0);
                    }
                }
            }
            else // Player gets object
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = StoveState.Idle;
                OnStateChanged?.Invoke(state);
                OnProgressChanged?.Invoke(0);
            }
        }
    }

    private KitchenObjectSO FindCookedObjectFromRawObject(KitchenObject rawObject)
    {
        FryingRecipeSO recipeSO = GetFryingRecipe(rawObject);
        return recipeSO != null ? recipeSO.output : null;
    }

    private bool HasRecipeWithRawObject(KitchenObject rawObject)
    {
        FryingRecipeSO recipeSO = GetFryingRecipe(rawObject);

        return recipeSO != null;

    }

    private FryingRecipeSO GetFryingRecipe(KitchenObject rawObject)
    {
        KitchenObjectSO rawObjectSO = rawObject.GetKitchenObjectSO();
        foreach (FryingRecipeSO recipe in recipes)
        {
            if (recipe.input == rawObjectSO)
            {
                return recipe;
            }
        }
        return null;
    }

    private void HandleFryingTimer()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case StoveState.Idle:
                    break;
                case StoveState.Frying:
                    fryingTimer += Time.deltaTime;
                    float normalizedFryingProgress = GetNormalizedProgress(fryingTimer, currentRecipe.fryingTimerMax);
                    OnProgressChanged?.Invoke(normalizedFryingProgress);

                    // Fried
                    if (fryingTimer >= currentRecipe.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(currentRecipe.output, this);
                        state = StoveState.Fried;
                        OnStateChanged?.Invoke(state);

                    }
                    break;
                case StoveState.Fried:
                    burningTimer += Time.deltaTime;
                    float normalizedBurningProgress = GetNormalizedProgress(burningTimer, currentRecipe.burningTimerMax);
                    OnProgressChanged?.Invoke(normalizedBurningProgress);

                    // Fried
                    if (burningTimer >= currentRecipe.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(currentRecipe.burnedOutput, this);
                        state = StoveState.Burned;
                        OnStateChanged?.Invoke(state);
                        OnProgressChanged?.Invoke(0);
                    }
                    break;
                case StoveState.Burned:
                    break;
            }
        }
    }

    private float GetNormalizedProgress(float time, float maxTime)
    {
        return time / maxTime;
    }

}
