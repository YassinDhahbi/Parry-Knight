using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Taggable
{
    public float dmg;
    public bool reflected;

    public bool stuck;
    public TrailRenderer trailRenderer;

    public ProjectilePreset preset;

    [SerializeField]
    private List<Color> listOftrailColors;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Vector2 prePauseVelocity;

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        if (reflected && !stuck)
        {
            spriteRenderer.transform.Rotate(Vector3.forward * Time.deltaTime * 360f);
        }
        if (GameManager.Instance.gameIsPaused)
        {
            rb2D.velocity = Vector3.zero;
        }
        if (!GameManager.Instance.gameIsPaused && !stuck)
        {
            rb2D.velocity = prePauseVelocity;
        }
    }

    public void AssignData()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        spriteRenderer.sprite = preset.projectileSprite;
        dmg = preset.damage;
        stuck = false;
        reflected = false;
        trailRenderer.startColor = Color.red;
        GetComponent<Collider2D>().enabled = true;
    }

    public void Shoot(Vector2 direction)
    {
        rb2D.AddForce(direction * preset.shootingForce, ForceMode2D.Impulse);
        prePauseVelocity = direction * preset.shootingForce;
    }

    public float GetDamageValue()
    {
        return dmg;
    }

    private IEnumerator DisableAfterAWhile(float nSeconds)
    {
        yield return new WaitForSeconds(nSeconds);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            rb2D.velocity = Vector2.zero;
            trailRenderer.enabled = false;
            rb2D.angularVelocity = 0;
            transform.GetComponent<Collider2D>().enabled = false;
            HitEffectManager.instance.SpawnHit(transform.position);
            stuck = true;
            StartCoroutine(DisableAfterAWhile(5));
        }

        if (taggingCondition.CheckForCompatibility(gameObject, other.gameObject))
        {
            if (reflected == false)
            {
                EventManager.Instance.OnProjectileDamageTaken.Raise(dmg);
                HitEffectManager.instance.DistributeBlood(transform.position, other.transform);
                HitEffectManager.instance.SpawnDamageNumber(transform.position, (int)dmg);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (other.TryGetComponent(out Damageable damagable) && reflected == true)
            {
                HitEffectManager.instance.DistributeBlood(transform.position, other.transform);
                damagable.Damage(dmg);
                HitEffectManager.instance.SpawnDamageNumber(transform.position, (int)dmg);
                gameObject.SetActive(false);
            }
        }
    }

    public void UpdateTrailEffect()
    {
        for (int i = 0; i < listOftrailColors.Count; i++)
        {
            if (dmg > i * 10)
            {
                trailRenderer.startColor = listOftrailColors[i];
            }
        }
    }

    public void PushInDirection(Vector2 direction, float reflectionForce)
    {
        prePauseVelocity = direction * reflectionForce;
    }
}