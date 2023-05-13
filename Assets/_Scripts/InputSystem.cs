using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSystem", menuName = "InputSystem/ Control Scheme")]
public class InputSystem : ScriptableObject
{
    public List<InputScheme> inputList;
    public InputAction GetInputSchemeByID(ControlIdentifier identifier)
    {

        foreach (var item in inputList)
        {
            if (item.id == identifier)
            {
                //Debug.Log(item.inputAction.ReadValue<Vector2>());
                return item.inputAction;
            }
        }
        return null;
    }

    public void EnableAllControls(bool state)
    {
        foreach (var item in inputList)
        {
            if (state == true)
            {

                item.inputAction.Enable();
            }
            else
            {
                item.inputAction.Disable();
            }

        }
    }
}
[System.Serializable]
public class InputScheme
{
    public ControlIdentifier id;
    public InputAction inputAction;
}

public enum ControlIdentifier
{
    Movement
}
