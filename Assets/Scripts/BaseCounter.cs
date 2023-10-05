using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform holdingObjectPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("Interact from Base Class shouldn't run");
    }

    // Interface
    public Transform GetParentHoldingPoint()
    {
        return holdingObjectPoint;
    }

    public void SetKitchenObject(KitchenObject obj)
    {
        kitchenObject = obj;
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
