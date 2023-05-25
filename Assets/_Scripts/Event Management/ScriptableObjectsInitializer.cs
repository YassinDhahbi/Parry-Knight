using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Tooltip("This is a class that is used as the monobehaviour that inializes the managers in all of the scriptable objects")]
public class ScriptableObjectsInitializer : MonoBehaviour
{
    private void Start()
    {
        HPManager.Instance.Initialize();
        InventoryManager.Instance.Enable();
        EventManager.Instance.OnGameStart.Raise();
        KeyInputManager.Instance.Subscription();

    }
    private void OnEnable()
    {
        CraftingManager.Instance.ResetFields();
    }
    private void OnDisable()
    {
        KeyInputManager.Instance.Unsubscription();
        InventoryManager.Instance.Disable();
    }
}
