using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO objectSO;
    [SerializeField] private Transform holdingObjectPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform objectTransform = Instantiate(objectSO.prefab, holdingObjectPoint);
            objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }

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
