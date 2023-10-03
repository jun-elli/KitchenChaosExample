using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter counter)
    {
        if (clearCounter != null)
        {
            clearCounter.ClearKitchenObject();
        }

        // Set new counter and check if empty
        clearCounter = counter;
        if (clearCounter.HasKitchenObject)
        {
            Debug.LogError("Counter is already full.");
        }

        // We tell the counter this object is now its child
        clearCounter.SetKitchenObject(this);
        transform.parent = counter.GetCounterCenterPoint();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
