using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tagging;
using ScriptableObjectArchitecture;
public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2D;
    RangedEnemy owner;
    float dmg;
    [SerializeField]
    TaggingCondition playerProjectileCondition;
    [SerializeField]
    FloatGameEvent OnPlayerDamaged;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void SetOwner(RangedEnemy parent)
    {
        owner = parent;
        dmg = owner.GetDamage();
    }
    public void Shoot(Vector2 direction, float shootingForce)
    {
        rb2D.AddForce(direction * shootingForce, ForceMode2D.Impulse);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerProjectileCondition.CheckForCompatibility(gameObject, other.gameObject))
        {
            OnPlayerDamaged.Raise(dmg);
            gameObject.SetActive(false);
        }
        else
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                // hurt the target
            }
        }
    }
}