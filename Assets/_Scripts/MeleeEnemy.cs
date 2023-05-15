using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tagging;
public class MeleeEnemy : MonoBehaviour, EnemyBehaviour
{
    bool reactionTriggered;
    Transform tragetTransform;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    TaggingCondition enemyPlayerCollision;
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
