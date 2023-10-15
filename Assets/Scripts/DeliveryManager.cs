
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeList;
    private List<RecipeSO> _waitingRecipeSOList;
    private float spawnRecipeTimer;
    private const float SPAWN_DELAY = 5F;
    private const int MAX_WAITING_RECIPES = 4;
    public int SuccessfullyDeliveredRecipesAmount { get; private set; }

    // Events
    public event Action OnRecipeSpawned;
    public event Action OnRightRecipeDelivered;
    public event Action OnWrongRecipeDelivered;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            _waitingRecipeSOList = new List<RecipeSO>();

        }
        SuccessfullyDeliveredRecipesAmount = 0;
    }

    private void Update()
    {
        HandleRecipeSpawning();
    }

    private void HandleRecipeSpawning()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = SPAWN_DELAY;

            if (GameManager.Instance.IsGamePlaying && _waitingRecipeSOList.Count < MAX_WAITING_RECIPES)
            {
                int randomIndex = UnityEngine.Random.Range(0, recipeList.recipeSOList.Count);
                RecipeSO waitingRecipe = recipeList.recipeSOList[randomIndex];
                _waitingRecipeSOList.Add(waitingRecipe);
                // Event
                OnRecipeSpawned?.Invoke();
            }
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plate)
    {
        // Order plate ingredients by name
        List<KitchenObjectSO> plateIngredients = plate.GetIngredientsOnPlate().OrderBy(i => i.objectName).ToList();
        // Cycle through each waiting recipe
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO recipe = _waitingRecipeSOList[i];
            // Have the same number of ingredients
            if (recipe.ingredientsSO.Count == plate.GetIngredientsOnPlate().Count)
            {
                // Order ingredients by name
                List<KitchenObjectSO> recipeIngredients = recipe.ingredientsSO.OrderBy(i => i.objectName).ToList();

                int sameIngredients = 0;
                // If they are the same, they'll be ordered the same way
                for (int j = 0; j < recipeIngredients.Count; j++)
                {
                    if (recipeIngredients[j].objectName == plateIngredients[j].objectName)
                    {
                        sameIngredients++;
                    }
                }
                // If all ingredients are the same
                if (sameIngredients == recipeIngredients.Count)
                {
                    // Recipe fulfilled
                    _waitingRecipeSOList.RemoveAt(i);
                    SuccessfullyDeliveredRecipesAmount++;
                    // Event
                    OnRightRecipeDelivered?.Invoke();
                    return true;
                }

            }
        }
        // Tried to deliver a recipe not allowed or not in the waiting list
        OnWrongRecipeDelivered?.Invoke();
        return false;
    }

    public List<RecipeSO> GetWaitingRecipesSO()
    {
        return _waitingRecipeSOList;
    }
}
