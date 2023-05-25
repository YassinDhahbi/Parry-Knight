using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{

    private void OnEnable()
    {
        Subscription();
        Debug.Log("Here");
    }
    private void OnDisable()
    {
        Unsubscription();
    }

    void Subscription()
    {
        #region Tab Press
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.TabMenu).performed += context =>
        {
            EventManager.Instance.OnInventoryOpenClose.Raise();
        };
        #endregion

    }

    void Unsubscription()
    {
        #region Tab Press
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.TabMenu).performed -= context => { EventManager.Instance.OnInventoryOpenClose.Raise(); };
        #endregion

    }

}
