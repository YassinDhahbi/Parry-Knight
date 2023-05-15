using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Transform itemHolderInUI;

    [SerializeField]
    InventorySpace inventorySpace;


    public void Add(PickablePreset pickedPreset)
    {
        inventorySpace.Add(pickedPreset, itemHolderInUI);
    }

    public void OpenCloseInventory(GameObject inventory)
    {
        inventory.SetActive(!inventory.activeInHierarchy);
    }


}

