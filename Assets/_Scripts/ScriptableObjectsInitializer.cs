using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Tooltip("This is a class that is used as the monobehaviour that inializes the managers in all of the scriptable objects")]
public class ScriptableObjectsInitializer : MonoBehaviour
{
    [SerializeField]
    HPManager hPManager;
    [SerializeField]
    InventorySpace inventorySpace;
    private void Awake()
    {
        hPManager.Initialize();
        inventorySpace.Reset();
    }
}
