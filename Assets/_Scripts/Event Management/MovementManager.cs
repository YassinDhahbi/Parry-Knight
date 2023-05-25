using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MovementManager", menuName = "Managers/ MovementManager")]
public class MovementManager : ScriptableObject
{
    #region Variables
    [SerializeField]
    float movementSpeed;
    Vector2 movementDirection;
    #endregion
    private void OnEnable()
    {
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.Movement).performed += ctx => GetMovementDirection();
    }
    private void OnDisable()
    {
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.Movement).performed -= ctx => GetMovementDirection();
    }

    void GetMovementDirection()
    {
        movementDirection = InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.Movement).ReadValue<Vector2>();
        EventManager.Instance.OnPlayerMove.Raise();
    }

    #region Movement
    public void MovePlayer(Rigidbody2D rb)
    {
        rb.velocity = movementDirection * movementSpeed;
    }
    #endregion

}
