using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryManager", menuName = "Managers/Inventory Manager")]
public class InventoryManager : ScriptableObjectSingleton<InventoryManager>
{

    #region Variables
    [SerializeField]
    GameObject itemsHolderInUI;
    [SerializeField]
    InventoryDescriptionManager descriptionManager;
    [SerializeField]
    List<Item> listOfHeldItems;

    [SerializeField]
    List<InventoryUIItem> listOfItemsInInventory;
    #endregion



    #region Monobehaviour
    public void Enable()
    {
        listOfHeldItems.Clear();
        listOfItemsInInventory.Clear();
    }
    public void Disable()
    {
        listOfHeldItems.Clear();
        listOfItemsInInventory.Clear();
    }
    #endregion

    #region Methods
    public void SetItemHolder(GameObject target)
    {
        itemsHolderInUI = target;
        foreach (Transform item in itemsHolderInUI.transform)
        {
            var UiItemScript = item.GetComponent<InventoryUIItem>();
            listOfItemsInInventory.Add(UiItemScript);
        }
    }

    public void SetDescriptionPanel(GameObject descriptionPanel)
    {
        descriptionManager = descriptionPanel.GetComponent<InventoryDescriptionManager>();
    }
    public void AddItem(GameObject pickedItem)
    {
        var itemPreset = pickedItem.GetComponent<Pickable>().GetPreset();
        var itemDetails = itemPreset.itemBaseDetails;
        Item newItem = new Item(itemDetails);
        CheckForDuplicates(newItem);
    }

    public void CheckForDuplicates(Item pickedItem)
    {
        bool exists = false;
        Item equivalentItem = null;
        foreach (var item in listOfHeldItems)
        {
            if (item.baseDetails == pickedItem.baseDetails)
            {
                exists = true;
                equivalentItem = item;
            }

        }
        if (exists == false)
        {
            listOfHeldItems.Add(pickedItem);
            HandleUIItemAddition(pickedItem);
        }
        else
        {
            equivalentItem.UpdateCount();
            FindItemInUIInventory(equivalentItem).UpdateCount();
        }
    }

    void HandleUIItemAddition(Item pickedItem)
    {
        foreach (var item in listOfItemsInInventory)
        {
            if (item.GetItemData().IsValid())
            {
                item.SetItemDetails(pickedItem);
                return;
            }
        }
    }
    InventoryUIItem FindItemInUIInventory(Item pickedItem)
    {
        foreach (var item in listOfItemsInInventory)
        {
            if (item.GetItemData().baseDetails == pickedItem.baseDetails)
            {
                return item;
            }
        }
        return null;
    }
    public void OpenCloseInventory(GameObject inventory)
    {
        inventory.SetActive(!inventory.activeInHierarchy);
    }


    public void AssignDescription(GameObject selectedItem)
    {
        var selectedItemScript = selectedItem.GetComponent<InventoryUIItem>();
        if (selectedItemScript.GetItemData().IsValid() == false)
        {
            descriptionManager.CopyItemData(selectedItemScript.GetItemData());
        }
    }

    public int GetCountOf(ItemBaseDetails itemBaseDetails)
    {
        foreach (var item in listOfHeldItems)
        {
            if (item.baseDetails.itemName == itemBaseDetails.itemName)
            {

                return item.GetCount();
            }
        }
        return 0;
    }

    public bool ReduceCountOfRecipe(ItemBaseDetails itemBaseDetails, int deductedCount)
    {
        foreach (var item in listOfHeldItems)
        {
            if (item.baseDetails == itemBaseDetails)
            {
                if (item.GetCount() >= deductedCount)
                {
                    item.ModifyCount(deductedCount);
                    UpdateTextDetails(item);
                    return true; // Exit the method once the count is deducted
                }
                else
                {
                    Debug.LogWarning("Not enough count to deduct for item: " + itemBaseDetails.itemName);
                    return false; // Exit the method if there is not enough count
                }

            }

        }
        return false;
    }


    public bool CheckForResourcesCompatibility(ItemBaseDetails itemBaseDetails, int deductedCount)
    {
        bool checkResult = false;
        foreach (var item in listOfHeldItems)
        {
            if (item.baseDetails == itemBaseDetails)
            {
                checkResult = item.GetCount() >= deductedCount;
            }
        }
        return checkResult;
    }

    void UpdateTextDetails(Item targetItem)
    {
        for (int i = 0; i < listOfHeldItems.Count; i++)
        {
            if (listOfItemsInInventory[i].TryGetComponent(out InventoryUIItem inventoryUIItem) && inventoryUIItem.GetItemData().baseDetails == targetItem.baseDetails)
            {
                inventoryUIItem.SetItemDetails(targetItem);
            }
        }

    }
    public void RemoveItem(ItemBaseDetails itemBaseDetails)
    {
        foreach (var item in listOfHeldItems)
        {
            if (item.baseDetails == itemBaseDetails)
            {
                listOfHeldItems.Remove(item);

            }
        }
    }


    public void SpawnNewItem(ItemBaseDetails itemBaseDetails)
    {
        Item newItem = new Item(itemBaseDetails);
        CheckForDuplicates(newItem);
    }
    #endregion
}
#region Extra Classes
[System.Serializable]
public class Item
{
    public ItemBaseDetails baseDetails;
    [SerializeField]
    private int count;

    public Item(ItemBaseDetails baseDetails)
    {
        this.baseDetails = baseDetails;
        count = 1;
    }

    public void UpdateCount()
    {
        count++;
    }

    public int GetCount()
    {
        return count;
    }

    public void ModifyCount(int amount)
    {
        count -= amount;
    }
    public bool IsValid()
    {
        return baseDetails.itemName == string.Empty;
    }

    public void SetCount(int newValue)
    {
        count = newValue;
    }
    #endregion


}
