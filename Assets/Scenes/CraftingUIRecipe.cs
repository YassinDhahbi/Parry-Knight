using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingUIRecipe : MonoBehaviour
{
    [SerializeField]
    Recipe recipe;

    [SerializeField]
    Image recipeImage;

    public void FillData(Recipe filledRecipe)
    {
        recipe = filledRecipe;
        recipeImage.sprite = recipe.GetItemData().itemSprite;
    }

    public Recipe GetRecipe()
    {
        return recipe;
    }
}
