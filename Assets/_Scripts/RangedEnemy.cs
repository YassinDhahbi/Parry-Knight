using System;
using System.Collections;
using System.Collections.Generic;
using Tagging;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, EnemyBehaviour
{
    [SerializeField]
    private float shootingRate;
    [SerializeField]
    private float shootingForce;
    [SerializeField]
    private Projectile bulletPrefab;
    [SerializeField]
    TaggingCondition enemyPlayerCollision;
    [SerializeField]
    float damagePerShot;
    [SerializeField]
    ProjectilePreset projectileSettings;
    private Transform tragetTransform;
    private bool reactionTriggered;

    public void Behaviour(Transform player)
    {
        tragetTransform = player;
        InvokeRepeating("ShootingBehaviour", 0, shootingRate);
    }

    private void ShootingBehaviour()
    {
        if (reactionTriggered == true)
        {
            Projectile newBullet = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Projectile>();
            newBullet.SetOwner(this);
            Vector3 shootingDirection = (tragetTransform.position - transform.position).normalized;
            newBullet.Shoot(shootingDirection, shootingForce);
        }
        else
        {
            CancelInvoke("ShootingBehaviour");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyPlayerCollision.CheckForCompatibility(gameObject, other.gameObject) && reactionTriggered == false)
        {
            reactionTriggered = true;
            Behaviour(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (enemyPlayerCollision.CheckForCompatibility(gameObject, other.gameObject) && reactionTriggered == true)
        {
            reactionTriggered = false;
        }
    }

    #region Setter & Getters
    public float GetDamage()
    {
        return damagePerShot;
    }

    #endregion
}


