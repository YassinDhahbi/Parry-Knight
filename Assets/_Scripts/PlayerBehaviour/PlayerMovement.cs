using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 movementDirection;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private bool isPenalized;

    private float initialMovementSpeed;
    private Rigidbody2D playerRb;
    private AnimationSwitcher playerAnimationSwitcher;

    private void Awake()
    {
        initialMovementSpeed = movementSpeed;
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimationSwitcher = GetComponent<AnimationSwitcher>();
    }

    public void MovePlayer()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            movementDirection = InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.Movement).ReadValue<Vector2>();
            playerRb.velocity = movementDirection * movementSpeed;
            playerAnimationSwitcher.SyncAnim();
        }
        else
        {
            playerRb.velocity = Vector2.zero;
        }
    }

    public void MovementPenalty(float movementMultiplier)
    {
        movementSpeed = initialMovementSpeed * movementMultiplier;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}