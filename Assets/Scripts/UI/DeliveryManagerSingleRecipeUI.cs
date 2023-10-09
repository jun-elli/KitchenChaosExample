using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleRecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipe)
    {
        recipeNameText.text = recipe.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (KitchenObjectSO ingredient in recipe.ingredientsSO)
        {
            Transform newIconTemplate = Instantiate(iconTemplate, iconContainer);
            newIconTemplate.gameObject.SetActive(true);
            newIconTemplate.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}
