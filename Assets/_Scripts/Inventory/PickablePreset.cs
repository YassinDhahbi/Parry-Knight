using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Item", menuName = "Pickable Item Preset")]
public class PickablePreset : ScriptableObject
{

    public ItemBaseDetails itemBaseDetails;
    public void AssignItemPresetInUI(Transform targetItem)
    {
        name = itemBaseDetails.itemName;
        var itemImage = targetItem.GetComponent<Image>();
        itemImage.sprite = this.itemBaseDetails.itemSprite;
        var itemDisplayText = targetItem.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDisplayText.text = this.itemBaseDetails.itemName;
    }

    public void AssignItemPresetInGame(Transform targetItem)
    {
        var itemImage = targetItem.GetComponent<SpriteRenderer>();
        itemImage.sprite = itemBaseDetails.itemSprite;
    }

}

[System.Serializable]
public class ItemBaseDetails
{
    public string itemName;
    public Sprite itemSprite;
    public string description;
}
