using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUIItem : MonoBehaviour
{
    [SerializeField]
    Item uiItemDetails;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TextMeshProUGUI countTMP;


    public void SetItemDetails(Item itemDetails)
    {
        uiItemDetails = itemDetails;
        itemImage.sprite = uiItemDetails.baseDetails.itemSprite;
        countTMP.text = uiItemDetails.GetCount().ToString();
        countTMP.gameObject.SetActive(true);
    }

    public Item GetItemData()
    {
        return uiItemDetails;
    }
    public void UpdateCount()
    {
        countTMP.text = uiItemDetails.GetCount().ToString();
    }
}
