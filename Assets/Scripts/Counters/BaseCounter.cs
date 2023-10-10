using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform holdingObjectPoint;

    private KitchenObject kitchenObject;

    // Events
    public static event Action<BaseCounter> OnAnyObjectPlacedHere;
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    public virtual void Interact(Player player)
    {
        Debug.LogError("Interact from Base Class shouldn't run");
    }

    public virtual void InteractAlternate(Player player)
    {
        // Debug.LogError("Interact Alternate from Base Class shouldn't run");
    }

    // Interface
    public Transform GetParentHoldingPoint()
    {
        return holdingObjectPoint;
    }

    public void SetKitchenObject(KitchenObject obj)
    {
        kitchenObject = obj;
        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }


}
