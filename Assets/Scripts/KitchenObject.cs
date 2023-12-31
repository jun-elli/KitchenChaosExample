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

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO objectSO, IKitchenObjectParent parent)
    {
        Transform objectTransform = Instantiate(objectSO.prefab);
        KitchenObject kitchenObject = objectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plate)
    {
        if (this is PlateKitchenObject)
        {
            plate = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plate = null;
            return false;
        }
    }
}
