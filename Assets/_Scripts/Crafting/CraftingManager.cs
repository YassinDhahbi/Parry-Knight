using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script defines the Crafting Manager as a ScriptableObject singleton.
// It can be created from the Unity editor and accessed throughout the game.
[CreateAssetMenu(fileName = "CraftingManager", menuName = "Managers/CraftingManager")]
public class CraftingManager : ScriptableObjectSingleton<CraftingManager>
{
    [SerializeField]
    List<Recipe> listOfRecipes;
    [SerializeField]
    CraftingUIManager craftingUIManager;
    [SerializeField]
    Recipe currentSelectedRecipe;
    public void FillRecipesInUI(Transform container, CraftingUIRecipe craftingUIRecipe, CraftingUIManager craftingUIManagerInput)
    {
        craftingUIManager = craftingUIManagerInput;
        foreach (var item in listOfRecipes)
        {
            CraftingUIRecipe newRecipe = Instantiate(craftingUIRecipe, container);
            newRecipe.FillData(item);
        }
    }
    public void SetData(CraftingUIRecipe UiRecipe)
    {
        currentSelectedRecipe = UiRecipe.GetRecipe();
        craftingUIManager.craftingPanelContent.SetRecipeData(UiRecipe.GetRecipe());
    }

    public void CraftingButtonBehaviour()
    {
        currentSelectedRecipe.Craft();
        craftingUIManager.craftingPanelContent.SetRecipeData(currentSelectedRecipe);
    }

    public void ResetFields()
    {
        currentSelectedRecipe = null;
    }
}
