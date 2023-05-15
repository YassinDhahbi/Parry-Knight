using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{

    #region Variables
    [SerializeField]
    Vector2GameEvent OnMovementPerformed;
    [SerializeField]
    GameEvent openCloseInventory;
    [SerializeField]
    InputSystem inputSystem;

    InputAction movementInput;
    InputAction tabPress;
    #endregion

    private void Awake()
    {
        movementInput = inputSystem.GetInputSchemeByID(ControlIdentifier.Movement);
        tabPress = inputSystem.GetInputSchemeByID(ControlIdentifier.TabMenu);
    }
    #region Initialization
    private void OnEnable()
    {
        Subscription();
        inputSystem.EnableAllControls(true);
    }
    private void OnDisable()
    {
        Unsubscription();
        inputSystem.EnableAllControls(false);
    }
    #endregion

    #region Player Movement Controls & Events

    void Subscription()
    {
        #region Movement
        movementInput.performed += context => OnMovementPerformed.Raise(movementInput.ReadValue<Vector2>());
        movementInput.canceled += context => OnMovementPerformed.Raise(Vector2.zero);
        #endregion

        #region Tab Press
        tabPress.performed += context => openCloseInventory.Raise();
        #endregion

    }

    void Unsubscription()
    {
        #region Movement
        movementInput.performed -= context => OnMovementPerformed.Raise(movementInput.ReadValue<Vector2>());
        movementInput.canceled -= context => OnMovementPerformed.Raise(Vector2.zero);
        #endregion
        #region Tab Press
        tabPress.performed -= context => openCloseInventory.Raise();
        #endregion
    }
    #endregion

}
