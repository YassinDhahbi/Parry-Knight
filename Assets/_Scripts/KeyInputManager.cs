using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/KeyInputManager")]
public class KeyInputManager : ScriptableObject
{

    public void Subscription()
    {
        // #region Movement
        // movementInput.performed += context => EventManager.Instance.OnPlayerMove.Raise(movementInput.ReadValue<Vector2>());
        // movementInput.canceled += context => EventManager.Instance.OnPlayerMove.Raise(Vector3.zero);
        // #endregion

        #region Tab Press
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.TabMenu).performed += context =>
        {
            EventManager.Instance.OnInventoryOpenClose.Raise();
        };
        #endregion


    }

    public void Unsubscription()
    {
        // #region Movement
        // movementInput.performed += context => EventManager.Instance.OnPlayerMove.Raise(movementInput.ReadValue<Vector2>());
        // movementInput.canceled += context => EventManager.Instance.OnPlayerMove.Raise(Vector3.zero);
        // #endregion

        #region Tab Press
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.TabMenu).performed -= context => { EventManager.Instance.OnInventoryOpenClose.Raise(); };
        #endregion

    }
}
