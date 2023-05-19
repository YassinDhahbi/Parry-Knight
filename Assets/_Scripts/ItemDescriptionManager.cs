using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemDescriptionManager : MonoBehaviour
{
    [SerializeField]
    Image currentSelectedItemImage;
    [SerializeField]
    TextMeshProUGUI currentSelectedItemDescription;

    public void UpdateCurrentDisplayedInfo(GameObject obj)
    {
        var currentSelectedItem = obj.GetComponent<UIItem>();
        UpdateTextAndImage(currentSelectedItem.preset);
    }
    void UpdateTextAndImage(PickablePreset itemPreset)
    {
        currentSelectedItemImage.sprite = itemPreset.itemSprite;
        currentSelectedItemDescription.text = itemPreset.description;
    }

}
