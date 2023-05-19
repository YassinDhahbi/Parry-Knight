using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ScriptableObjectArchitecture;
public class UIItem : MonoBehaviour
{
    [SerializeField]
    Image uiItemImage;
    [SerializeField]
    TextMeshProUGUI itemNameTMP;
    [SerializeField]
    TextMeshProUGUI itemCountTMP;
    int itemCount;
    public PickablePreset preset;
    [SerializeField]
    GameObjectGameEvent OnSelect;
    public void UpdateCount()
    {
        itemCount++;
        itemCountTMP.text = itemCount.ToString();
    }

    public void SetPresetDetails(PickablePreset addPreset)
    {
        uiItemImage.sprite = addPreset.itemSprite;
        itemNameTMP.text = addPreset.name;
        preset = addPreset;
        UpdateCount();
    }
    public int GetCount()
    {
        return itemCount;
    }

    public void Selectable()
    {
        OnSelect.Raise(gameObject);
    }
}

