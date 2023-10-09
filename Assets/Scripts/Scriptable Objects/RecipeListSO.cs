using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom SO/New Recipe List", fileName = "Recipe List")]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
