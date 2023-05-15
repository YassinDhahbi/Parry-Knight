using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName = "Managers/ Inventory")]
public class InventorySpace : ScriptableObject
{

    [SerializeField]
    List<ItemSlot> itemSlotsList;
    [SerializeField]
    private GameObject itemUIPrefab;

    public bool Add(PickablePreset itemId, Transform itemHolderInUI)
    {


        foreach (var item in itemSlotsList)
        {
            if (item.itemPreset == itemId)
            {
                item.UpdateCount();
                return false;
            }
        }
        GameObject spawnedUI = Instantiate(itemUIPrefab, itemHolderInUI);
        itemId.AssignItemPresetInUI(spawnedUI.transform);
        var counterTMP = spawnedUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        var newItem = new ItemSlot(itemId);
        newItem.SetTMPCounter(counterTMP);
        newItem.UpdateCount();
        itemSlotsList.Add(newItem);


        return true;

    }

    public void Reset()
    {
        itemSlotsList.Clear();
    }
}


[System.Serializable]
public class ItemSlot
{
    public PickablePreset itemPreset;
    [SerializeField]
    int count;

    [SerializeField]
    TextMeshProUGUI counterTMP;

    #region Setter & Getters
    public void UpdateCount()
    {
        count++;
        counterTMP.text = count.ToString();
    }
    public int GetCount()
    {
        return count;
    }
    #endregion
    public ItemSlot(PickablePreset item)
    {
        itemPreset = item;
    }
    public void SetTMPCounter(TextMeshProUGUI counter)
    {
        counterTMP = counter;
    }
}