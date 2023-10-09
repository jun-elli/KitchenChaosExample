using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;

        UpdateWaitingRecipesUI();
    }

    private void DeliveryManager_OnRecipeSpawned()
    {
        UpdateWaitingRecipesUI();
    }

    private void DeliveryManager_OnRecipeDelivered()
    {
        UpdateWaitingRecipesUI();
    }

    private void UpdateWaitingRecipesUI()
    {
        // Clean if others remain
        foreach (Transform child in container)
        {
            if (child != recipeTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipe in DeliveryManager.Instance.GetWaitingRecipesSO())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleRecipeUI>().SetRecipeSO(recipe);
        }

    }


}

