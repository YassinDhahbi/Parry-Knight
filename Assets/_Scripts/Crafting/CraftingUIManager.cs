using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
    // Reference to the CraftingPanelContent script
    public CraftingPanelContent craftingPanelContent;

    // Container for holding the recipe UI elements
    [SerializeField]
    Transform container;

    // Prefab for the CraftingUIRecipe element
    [SerializeField]
    CraftingUIRecipe recipePrefab;

    private void Start()
    {
        // Fill the recipes in the UI using the CraftingManager instance
        CraftingManager.Instance.FillRecipesInUI(container, recipePrefab, this);
    }
}

[System.Serializable]
public class CraftingPanelContent
{
    [Header("Text Section")]
    [SerializeField]
    TextMeshProUGUI nameTMP; // TextMeshProUGUI for displaying the name
    [SerializeField]
    TextMeshProUGUI descriptionTMP; // TextMeshProUGUI for displaying the description

    [Header("Image Section")]
    [SerializeField]
    Image spriteInDetailsPanel; // Image for displaying the sprite in the details panel
    [SerializeField]
    Image spriteInCraftingPanel; // Image for displaying the sprite in the crafting panel

    [SerializeField]
    GameObject ingredientsContainer; // Container for holding the ingredient UI elements

    // Set the recipe data in the UI
    public void SetRecipeData(Recipe recipe)
    {
        // Get the base details of the recipe's item
        ItemBaseDetails baseDetails = recipe.GetItemData();

        // Set the text and image data in the UI elements
        nameTMP.text = baseDetails.itemName;
        descriptionTMP.text = baseDetails.description;
        spriteInCraftingPanel.sprite = baseDetails.itemSprite;
        spriteInDetailsPanel.sprite = baseDetails.itemSprite;

        // Set the recipe UI in the ingredients container
        if (recipe)
        {
            recipe.SetRecipeUI(ingredientsContainer);
        }
    }
}
