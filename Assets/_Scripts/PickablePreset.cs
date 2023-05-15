using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Item", menuName = "Pickable Item Preset")]
public class PickablePreset : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string description;

    public void AssignItemPresetInUI(Transform targetItem)
    {
        var itemImage = targetItem.GetComponent<Image>();
        itemImage.sprite = this.itemSprite;
        var itemDisplayText = targetItem.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDisplayText.text = this.itemName;
    }

    public void AssignItemPresetInGame(Transform targetItem)
    {
        var itemImage = targetItem.GetComponent<SpriteRenderer>();
        itemImage.sprite = itemSprite;
    }

}
