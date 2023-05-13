using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, EnemyBehaviour
{
    [SerializeField]
    float shootingRate;
    [SerializeField]
    float shootingForce;
    [SerializeField]
    Rigidbody2D bulletPrefab;
    Transform tragetTransform;
    bool reactionTriggered;
    public void Behaviour(Transform player)
    {
        tragetTransform = player;
        InvokeRepeating("ShootingBehaviour", 0, shootingRate);
    }

    private void ShootingBehaviour()
    {
        Rigidbody2D newBullet = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        Vector3 shootingDirection = (tragetTransform.position - transform.position).normalized;
        newBullet.AddForce(shootingDirection * shootingForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && reactionTriggered == false)
        {
            reactionTriggered = true;
            Behaviour(other.transform);
        }
    }



}
