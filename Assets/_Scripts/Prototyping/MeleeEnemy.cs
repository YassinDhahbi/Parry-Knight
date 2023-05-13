using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour, EnemyBehaviour
{
    bool reactionTriggered;
    Transform tragetTransform;
    [SerializeField]
    float movementSpeed;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Behaviour(Transform player)
    {
        tragetTransform = player;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && reactionTriggered == false)
        {
            reactionTriggered = true;
            Behaviour(other.transform);
        }
    }

    void FollowBehaviour()
    {
        if (reactionTriggered)
        {
            transform.position = Vector3.Lerp(transform.position, tragetTransform.position, Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        FollowBehaviour();
    }
}
