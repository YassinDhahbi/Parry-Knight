using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryDescriptionManager : MonoBehaviour
{
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemDescription;

    public void CopyItemData(Item item)
    {
        itemImage.sprite = item.baseDetails.itemSprite;
        itemName.text = item.baseDetails.itemName;
        itemDescription.text = item.baseDetails.description;
    }
}
