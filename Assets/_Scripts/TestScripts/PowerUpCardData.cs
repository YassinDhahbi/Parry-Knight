using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

[CreateAssetMenu(menuName = "PowerUpCard/Data")]
public class PowerUpCardData : ScriptableObject
{
    public float buffValue;

    [SerializeField]
    private string powerUpUnit;

    [SerializeField]
    private string powerUpType;

    [SerializeField]
    private Sprite gemSprite;

    [SerializeField]
    private Color textColor;

    public FloatGameEvent buffEvent;

    public Sprite GetSprite()
    {
        return gemSprite;
    }

    public void Init(Image image, TMPro.TextMeshProUGUI descriptionText)
    {
        image.sprite = GetSprite();
        descriptionText.text = "+" + buffValue.ToString() + powerUpUnit + " " + powerUpType;
        textColor.a = 1;
        descriptionText.color = textColor;
    }

    public void OnCardClicked()
    {
        buffEvent.Raise(buffValue);
    }
}