using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom SO/New Recipe", fileName = "Recipe")]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> ingredientsSO;

    public string recipeName;
}
