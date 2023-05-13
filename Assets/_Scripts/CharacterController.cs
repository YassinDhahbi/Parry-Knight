using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class CharacterController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float movementSpeed;
    #endregion

    #region Movement
    public void MovementBehaviour(Vector2 movementInput)
    {
        rb.velocity = movementInput * movementSpeed;
    }
    #endregion


}
