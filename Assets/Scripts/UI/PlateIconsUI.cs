using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private Transform iconTemplate;

    private List<Transform> iconsList;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plate.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectSO objectSO)
    {
        UpdateIcons();
    }

    private void UpdateIcons()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO objectSO in plate.GetIngredientsOnPlate())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetIcon(objectSO);
        }
    }
}
