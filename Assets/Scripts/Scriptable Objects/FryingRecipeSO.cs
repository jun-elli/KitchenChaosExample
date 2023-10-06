using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom SO/New Frying Recipe", fileName = "Frying Recipe")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public KitchenObjectSO burnedOutput;
    public float fryingTimerMax;
    public float burningTimerMax;
}
