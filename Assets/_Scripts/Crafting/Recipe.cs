using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Recepie", menuName = "Craftable Recepie")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    ItemBaseDetails baseData;

    [SerializeField]
    List<RecipeIngredient> listOfIngredients;
    public ItemBaseDetails GetItemData()
    {
        return baseData;
    }
    public void SetRecipeUI(GameObject ingredientsUIContainer)
    {
        for (int i = 0; i < listOfIngredients.Count; i++)
        {
            if (listOfIngredients[i] == null)
            {
                return;
            }
            var tmp = ingredientsUIContainer.transform.GetChild(i).GetComponentInChildren<TMPro.TextMeshProUGUI>();
            var img = ingredientsUIContainer.transform.GetChild(i).GetChild(1).GetComponent<Image>();
            var currentCount = InventoryManager.Instance.GetCountOf(listOfIngredients[i].GetIngredient().itemBaseDetails);
            var countRequired = listOfIngredients[i].GetNeededAmount();
            tmp.text = currentCount + " / " + countRequired;
            img.sprite = listOfIngredients[i].GetIngredient().itemBaseDetails.itemSprite;
        }
    }
    public void Craft()
    {
        bool craftable = true;
        foreach (var item in listOfIngredients)
        {
            craftable &= InventoryManager.Instance.CheckForResourcesCompatibility(item.GetIngredient().itemBaseDetails, item.GetNeededAmount());
        }
        if (craftable == true)
        {
            foreach (var item in listOfIngredients)
            {
                craftable &= InventoryManager.Instance.ReduceCountOfRecipe(item.GetIngredient().itemBaseDetails, item.GetNeededAmount());
            }
            Item newItem = new Item(baseData);
            InventoryManager.Instance.CheckForDuplicates(newItem);
        }
        else
        {
            foreach (var item in listOfIngredients)
            {
                InventoryManager.Instance.CheckForResourcesCompatibility(item.GetIngredient().itemBaseDetails, item.GetNeededAmount());
            }
        }

        Debug.Log("Craftable " + craftable);
    }
}




[System.Serializable]
public class RecipeIngredient
{
    [SerializeField]
    PickablePreset ingredient;

    [SerializeField]
    int neededAmount;

    public int GetNeededAmount()
    {
        return neededAmount;
    }
    public PickablePreset GetIngredient()
    {
        return ingredient;
    }
}