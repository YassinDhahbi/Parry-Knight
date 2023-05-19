using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Transform itemHolderInUI;

    [SerializeField]
    InventorySpace inventorySpace;

    public void Add(GameObject pickedItem)
    {
        PickablePreset pickedPreset = pickedItem.GetComponent<Pickable>().GetPreset();
        // inventorySpace.Add(pickedPreset, itemHolderInUI);
        inventorySpace.AddItem(pickedPreset, itemHolderInUI);
    }

    public void OpenCloseInventory(GameObject inventory)
    {
        inventory.SetActive(!inventory.activeInHierarchy);
    }


}

