using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script defines the Crafting Manager as a ScriptableObject singleton.
// It can be created from the Unity editor and accessed throughout the game.
[CreateAssetMenu(fileName = "CraftingManager", menuName = "Managers/CraftingManager")]
public class CraftingManager : ScriptableObjectSingleton<CraftingManager>
{
    // List of recipes available in the game
    [SerializeField]
    List<Recipe> listOfRecipes;

    // Reference to the CraftingUIManager script
    [SerializeField]
    CraftingUIManager craftingUIManager;

    // The currently selected recipe
    [SerializeField]
    Recipe currentSelectedRecipe;

    // Fills the crafting UI with recipe data
    public void FillRecipesInUI(Transform container, CraftingUIRecipe craftingUIRecipe, CraftingUIManager craftingUIManagerInput)
    {
        craftingUIManager = craftingUIManagerInput;

        // Iterate through each recipe and instantiate a CraftingUIRecipe element in the container
        foreach (var item in listOfRecipes)
        {
            CraftingUIRecipe newRecipe = Instantiate(craftingUIRecipe, container);
            newRecipe.FillData(item);
        }
    }

    // Sets the currently selected recipe based on the selected CraftingUIRecipe
    public void SetData(CraftingUIRecipe UiRecipe)
    {
        currentSelectedRecipe = UiRecipe.GetRecipe();
        craftingUIManager.craftingPanelContent.SetRecipeData(UiRecipe.GetRecipe());
    }

    // Performs the crafting logic for the currently selected recipe
    public void CraftingButtonBehaviour()
    {
        currentSelectedRecipe.Craft();
        craftingUIManager.craftingPanelContent.SetRecipeData(currentSelectedRecipe);
    }

    // Resets the currently selected recipe
    public void ResetFields()
    {
        currentSelectedRecipe = null;
    }
}
