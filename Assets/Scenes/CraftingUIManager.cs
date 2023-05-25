using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftingUIManager : MonoBehaviour
{
    public CraftingPanelContent craftingPanelContent;

    [SerializeField]
    Transform container;

    [SerializeField]
    CraftingUIRecipe recipePrefab;

    private void Start()
    {
        CraftingManager.Instance.FillRecipesInUI(container, recipePrefab, this);
    }

}

[System.Serializable]
public class CraftingPanelContent
{
    [Header("Text Section")]
    [SerializeField]
    TextMeshProUGUI nameTMP;
    [SerializeField]
    TextMeshProUGUI descriptionTMP;
    [Header("Image Section")]
    [SerializeField]
    Image spriteInDetailsPanel;
    [SerializeField]
    Image spriteInCraftingPanel;
    [SerializeField]
    GameObject ingredientsContainer;

    public void SetRecipeData(Recipe recipe)
    {
        ItemBaseDetails baseDetails = recipe.GetItemData();
        nameTMP.text = baseDetails.itemName;
        descriptionTMP.text = baseDetails.description;
        spriteInCraftingPanel.sprite = baseDetails.itemSprite;
        spriteInDetailsPanel.sprite = baseDetails.itemSprite;
        if (recipe)
        {
            recipe.SetRecipeUI(ingredientsContainer);
        }

    }
}


