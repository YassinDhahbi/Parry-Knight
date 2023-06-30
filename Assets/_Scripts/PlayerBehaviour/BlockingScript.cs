using UnityEngine;

public class BlockingScript : Taggable
{
    #region Variables

    [SerializeField]
    private float perfectBlockTiming;

    [SerializeField]
    private float damageResistancePercentage;

    [SerializeField]
    private float relectingForce;

    [SerializeField]
    private float timer;

    [SerializeField]
    private Transform reflectionIndicator;

    [SerializeField]
    private float offSet;

    [SerializeField]
    private float reflectionForce;

    [SerializeField]
    private float movementMultiplierWhenShieldIsActive;

    [SerializeField]
    private Gradient projectileColorOnReflect;

    private bool timerIsActive;
    private Camera cam;
    private PlayerMovement playerMovementScript;

    #endregion Variables

    #region Monobehaviour

    private void Awake()
    {
        cam = Camera.main;
        playerMovementScript = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (taggingCondition.CheckForCompatibility(gameObject, other.gameObject))
        {
            ReflectProjectile(other);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            TimerManager();
            ManageReflectorDirection();
        }
    }

    #endregion Monobehaviour

    #region Methods

    private void ReflectProjectile(Collider2D other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            if (timer < perfectBlockTiming)
            {
                EventManager.Instance.OnPerfectBlock.Raise();
                projectile.reflected = true;
                var direction = FindMousePos() - reflectionIndicator.position;
                //projectile.Shoot(direction.normalized);
                projectile.PushInDirection(direction.normalized, reflectionForce);
                projectile.trailRenderer.colorGradient = projectileColorOnReflect;
                projectile.dmg += reflectionForce;
                projectile.UpdateTrailEffect();
                EventManager.Instance.OnBlockingExpCollected.Raise((int)projectile.dmg);
            }
            else
            {
                EventManager.Instance.OnProjectileDamageTaken.Raise(projectile.GetDamageValue() - damageResistancePercentage);
            }
        }
    }

    private void ManageReflectorDirection()
    {
        reflectionIndicator.gameObject.SetActive(timer < perfectBlockTiming && gameObject.activeInHierarchy);
        RotateIndicator();
    }

    private void RotateIndicator()
    {
        var direction = FindMousePos() - reflectionIndicator.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        reflectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle + offSet);
    }

    private Vector3 FindMousePos()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePosition);
        worldPos.z = 0f;
        return worldPos;
    }

    public void ShowShield()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        reflectionIndicator.gameObject.SetActive(gameObject.activeInHierarchy);
        timerIsActive = gameObject.activeInHierarchy;
        ShieldSlowDownEffect();
        if (timerIsActive == false)
        {
            timer = 0;
        }
    }

    public void ShieldSlowDownEffect()
    {
        var currentMovementPenalty = 1f;

        if (gameObject.activeInHierarchy)
        {
            currentMovementPenalty = movementMultiplierWhenShieldIsActive;
        }
        else
        {
            currentMovementPenalty = 1f;
        }
        playerMovementScript.MovementPenalty(currentMovementPenalty);
    }

    private void TimerManager()
    {
        if (timerIsActive == true)
        {
            timer += Time.deltaTime;
        }
    }

    public void UpdateBlockTiming(float t)
    {
        perfectBlockTiming += t;
    }
}

#endregion Methods