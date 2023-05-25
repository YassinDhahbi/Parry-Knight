using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : Taggable
{
    [SerializeField]
    PickablePreset itemPreset;

    private void Awake()
    {
        itemPreset.AssignItemPresetInGame(gameObject.transform);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (taggingCondition.CheckForCompatibility(gameObject, other.gameObject))
        {
            PickUpInteraction();
        }
    }


    public void PickUpInteraction()
    {
        gameObject.SetActive(false);
        EventManager.Instance.OnPlayerItemPickup.Raise(gameObject);
    }

    public PickablePreset GetPreset()
    {
        return itemPreset;
    }
}
