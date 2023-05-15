using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : Taggable
{
    public UnityEvent responseEvent;
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
            PickUpInteraction(other.gameObject);
            responseEvent.Invoke();
        }
    }


    public virtual void PickUpInteraction(GameObject other) { }
}
