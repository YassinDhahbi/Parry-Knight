using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName = "Managers/ Inventory")]
public class InventorySpace : ScriptableObject
{


    [SerializeField]
    private UIItem inventoryItemPrefab;
    List<UIItem> iventoryItemsList;
    [SerializeField]
    List<ItemSlot> itemSlotsList;

    public void Reset()
    {
        itemSlotsList.Clear();
        iventoryItemsList.Clear();
    }
    public void AddItem(PickablePreset itemPreset, Transform itemHolderInUI)
    {
        if (CheckForItemDuplicate(itemPreset))
        {
            UIItem newInventoryItem = Instantiate(inventoryItemPrefab, itemHolderInUI);
            newInventoryItem.SetPresetDetails(itemPreset);
            iventoryItemsList.Add(newInventoryItem);
        }
    }

    bool CheckForItemDuplicate(PickablePreset itemPreset)
    {
        var i = 0;
        if (iventoryItemsList.Count > 0)
        {
            foreach (var item in iventoryItemsList)
            {
                i++;
                if (item.preset == itemPreset)
                {
                    item.UpdateCount();
                    FindItemSlot(itemPreset).SetCount(item.GetCount());
                    return false;
                }
            }
        }
        itemSlotsList.Add(new ItemSlot(itemPreset, 1));
        return true;
    }

    ItemSlot FindItemSlot(PickablePreset itemPreset)
    {
        foreach (var item in itemSlotsList)
        {
            if (item.GetPreset() == itemPreset)
            {
                return item;
            }
        }
        return null;
    }
}


[System.Serializable]
public class ItemSlot
{
    [SerializeField]
    PickablePreset itemPreset;
    [SerializeField]
    int count;

    public ItemSlot(PickablePreset preset, int counter)
    {
        itemPreset = preset;
        count = counter;
    }
    public int GetCount()
    {
        return count;
    }
    public void SetCount(int x)
    {
        count = x;
    }
    public PickablePreset GetPreset()
    {
        return itemPreset;
    }
}