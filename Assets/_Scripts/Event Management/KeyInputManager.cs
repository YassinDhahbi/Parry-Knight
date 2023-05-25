using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/KeyInputManager")]
public class KeyInputManager : ScriptableObjectSingleton<KeyInputManager>
{

    public void Subscription()
    {
        #region Tab Press
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.TabMenu).performed += context =>
        {
            EventManager.Instance.OnInventoryOpenClose.Raise();
        };
        #endregion


    }

    public void Unsubscription()
    {
        #region Tab Press
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.TabMenu).performed -= context => { EventManager.Instance.OnInventoryOpenClose.Raise(); };
        #endregion

    }


}
