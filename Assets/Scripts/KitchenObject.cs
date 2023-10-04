using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent parent)
    {
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }

        // Set new counter and check if empty
        kitchenObjectParent = parent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Parent is already full.");
        }

        // We tell the counter this object is now its child
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = parent.GetParentHoldingPoint();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
