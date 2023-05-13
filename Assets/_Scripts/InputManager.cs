using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{

    #region Variables
    [SerializeField]
    Vector2GameEvent movementEvent;
    [SerializeField]
    InputSystem inputSystem;

    InputAction movementInput;
    #endregion

    private void Awake()
    {
        movementInput = inputSystem.GetInputSchemeByID(ControlIdentifier.Movement);
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
        movementInput.performed += context => movementEvent.Raise(movementInput.ReadValue<Vector2>());
        movementInput.canceled += context => movementEvent.Raise(Vector2.zero);
        #endregion

    }

    void Unsubscription()
    {
        #region Movement
        movementInput.performed -= context => movementEvent.Raise(movementInput.ReadValue<Vector2>());
        movementInput.canceled -= context => movementEvent.Raise(Vector2.zero);
        #endregion

    }
    #endregion

}
