using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO objectSO;
    [SerializeField] private Transform centerPoint;

    public bool HasKitchenObject => kitchenObject != null;

    private KitchenObject kitchenObject;

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform objectTransform = Instantiate(objectSO.prefab, centerPoint);
            objectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log("Counter full.");
        }
    }

    public Transform GetCounterCenterPoint()
    {
        return centerPoint;
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
}
