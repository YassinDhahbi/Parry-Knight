using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationSwitcher : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rb;

    private int hashedHorizontalWalkAnimId;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hashedHorizontalWalkAnimId = Animator.StringToHash("HorizontalWalking");
    }

    #endregion Variables

    #region Event Calls

    //This function is called in the event of the player movement
    public void SyncAnim()
    {
        HorizontalMovementAnimator(rb.velocity);
    }

    #endregion Event Calls

    #region Movement Behaviour

    // This function is used to take in all the possible movement types and act accordingly
    private void HorizontalMovementAnimator(Vector3 veclocity)
    {
        var velX = veclocity.x;
        var velY = veclocity.y;
        var movingRight = velX < 0;
        var movingLeft = velX > 0;
        var verticalRightMovement = movingRight && velY > 0;
        var verticalLeftMovement = movingLeft && velY < 0;
        var upDownMovement = (movingLeft == false && movingRight == false) && Mathf.Abs(velY) > 0;

        if (verticalRightMovement || movingRight)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            animator.SetBool(hashedHorizontalWalkAnimId, true);
        }
        else if (verticalLeftMovement || movingLeft)
        {
            transform.localScale = new Vector3(1, 1, 0);
            animator.SetBool(hashedHorizontalWalkAnimId, true);
        }
        else if (upDownMovement)
        {
            animator.SetBool(hashedHorizontalWalkAnimId, true);
        }
        else
        {
            animator.SetBool(hashedHorizontalWalkAnimId, false);
        }
    }

    #endregion Movement Behaviour
}