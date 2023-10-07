using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct VisualObjectToSOLink
    {
        public KitchenObjectSO objectSO;
        public GameObject objectVisual;
    }

    [SerializeField] private PlateKitchenObject plateObject;
    [SerializeField] private List<VisualObjectToSOLink> visualObjectToSOLinksList;

    private void Start()
    {
        plateObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectSO objectSO)
    {
        foreach (VisualObjectToSOLink link in visualObjectToSOLinksList)
        {
            if (link.objectSO == objectSO)
            {
                link.objectVisual.SetActive(true);
                return;
            }
        }
    }
}
