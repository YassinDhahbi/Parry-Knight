using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyStateMachine
{
    public float movementSpeed;
    public Transform target;
    protected bool isAttacking;
    protected bool isChasing;
    protected float shootingTimer;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private EnemyDataPreset enemyDataPreset;

    [SerializeField]
    private EnemyWeapon weapon;

    [SerializeField]
    private RangeColliders rangeColliders;

    private Animator animator;

    private int hashedHorizontalWalkAnimId;
    private int hashedBowAttackIdAnimId;
    private int hashedDeathIdAnimId;
    private SpriteRenderer enemyVisuals;

    #region MonoBehaviour

    private void Start()
    {
        currentState.EnterState(this);
        SetUpColliders();
        enemyVisuals = GetComponentInChildren<SpriteRenderer>();
        movementSpeed = enemyDataPreset.movementSpeed;
        AnimatorSetup();
        animator.speed = ((RangedEnemyDataPreset)enemyDataPreset).shootingRate;
        agent.isStopped = true;
        GameManager.Instance.UpdateNumberOfEnemies(1);
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    #endregion MonoBehaviour

    #region Inheritence Handling

    public override void Attack()
    {
        var direction = (target.position - transform.position);
        weapon.RotateWeapon(direction);
    }

    public void AttackBehaviour()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            var rangedDetails = (RangedEnemyDataPreset)enemyDataPreset;
            var direction = (target.position - transform.position);
            HorizontalMovementAnimator(direction.normalized);
            var shootingPoint = weapon.GetWeaponTransform().GetChild(0).GetChild(0);
            Projectile newProjectile = PoolingSystem.instance.GetProjectile();
            PoolingBehaviour(newProjectile, shootingPoint.eulerAngles, rangedDetails, direction);
        }
    }

    private void PoolingBehaviour(Projectile newProjectile, Vector3 rot, RangedEnemyDataPreset rangedDetails, Vector3 direction)
    {
        newProjectile.transform.eulerAngles = rot;
        newProjectile.transform.position = weapon.GetWeaponTransform().position;
        newProjectile.preset = rangedDetails.preset;
        newProjectile.AssignData();
        newProjectile.trailRenderer.enabled = true;
        newProjectile.Shoot(direction.normalized);
    }

    public override void Chase()
    {
        if (isChasing && GameManager.Instance.gameIsPaused == false)
        {
            agent.speed = enemyDataPreset.movementSpeed;
            agent.SetDestination(target.position);
            HorizontalMovementAnimator(agent.velocity);
        }
    }

    #endregion Inheritence Handling

    #region StateChecking

    public void SetAttack(bool status)
    {
        isAttacking = status;
    }

    public void SetChase(bool status)
    {
        isChasing = status;
    }

    public void SetAttackState(bool state)
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            animator.SetBool(hashedBowAttackIdAnimId, state);
        }
    }

    public void DeathBehaviour()
    {
        animator.SetTrigger(hashedDeathIdAnimId);
        this.enabled = false;
        agent.enabled = false;
        GameManager.Instance.SubtractEnemy();
    }

    #endregion StateChecking

    #region Setters & Getters

    private void AnimatorSetup()
    {
        animator = GetComponent<Animator>();
        hashedHorizontalWalkAnimId = Animator.StringToHash("HorizontalWalking");
        hashedBowAttackIdAnimId = Animator.StringToHash("BowAttack");
        hashedDeathIdAnimId = Animator.StringToHash("Die");
    }

    public void SetTransform(GameObject t)
    {
        target = t.transform;
    }

    #endregion Setters & Getters

    #region GizmosRepresentation

    private void OnDrawGizmos()
    {
        Gizmos.color = enemyDataPreset.attackRadiusColor;
        Gizmos.DrawWireSphere(transform.position, enemyDataPreset.attackRadius);
        Gizmos.color = enemyDataPreset.chaseRadiusColor;
        Gizmos.DrawWireSphere(transform.position, enemyDataPreset.chaseRadius);
    }

    #endregion GizmosRepresentation

    #region SetUp

    private void SetUpColliders()
    {
        rangeColliders.closeRangeCollider.radius = enemyDataPreset.attackRadius;
        rangeColliders.wideRangeCollider.radius = enemyDataPreset.chaseRadius;
    }

    #endregion SetUp

    #region Animation Handling

    private void HorizontalMovementAnimator(Vector3 veclocity)
    {
        if (GameManager.Instance.gameIsPaused == false)
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
                enemyVisuals.transform.localScale = new Vector3(-1, 1, 0);
                //transform.localScale = new Vector3(-1, 1, 0);
                animator.SetBool(hashedHorizontalWalkAnimId, true);
            }
            else if (verticalLeftMovement || movingLeft)
            {
                enemyVisuals.transform.localScale = new Vector3(1, 1, 0);
                //transform.localScale = new Vector3(1, 1, 0);
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
    }

    [System.Serializable]
    private struct RangeColliders
    {
        public CircleCollider2D closeRangeCollider;
        public CircleCollider2D wideRangeCollider;
    }

    #endregion Animation Handling
}

[System.Serializable]
public class EnemyWeapon
{
    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private Vector3 facingRightRotation;

    [SerializeField]
    private Vector3 facingLeftRotation;

    public void RotateWeapon(Vector2 direction)
    {
        // Calculate the rotation angle based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Set the rotation of the shooting point
        weaponHolder.localRotation = Quaternion.Euler(0f, 0f, angle);
        WeaponDirectionFixer(direction.x > 0);
    }

    private void WeaponDirectionFixer(bool isRight)
    {
        weaponHolder.localEulerAngles += isRight ? facingRightRotation : facingLeftRotation;
    }

    public Transform GetWeaponTransform()
    {
        return weaponHolder;
    }
}