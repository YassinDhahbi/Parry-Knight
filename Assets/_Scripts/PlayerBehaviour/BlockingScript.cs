using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingScript : Taggable
{
    #region Variables
    [SerializeField]
    float perfectBlockTiming;
    [SerializeField]
    float damageResistancePercentage;
    [SerializeField]
    float relectingForce;
    [SerializeField]
    float timer;
    bool timerIsActive;
    #endregion



    #region Monobehaviour
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (taggingCondition.CheckForCompatibility(gameObject, other.gameObject))
        {
            ReflectProjectile(other);
        }
    }

    private void Update()
    {
        TimerManager();
    }
    #endregion



    void ReflectProjectile(Collider2D other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            if (timer < perfectBlockTiming)
            {
                projectile.ShootingInRelectionDirection(relectingForce);
                Debug.Log("Perfect block");
                return;
            }
            EventManager.Instance.OnProjectileDamageTaken.Raise(projectile.GetDamageValue() - damageResistancePercentage);
            Debug.Log("Shield Hit");
            Destroy(other.gameObject);


        }

    }
    public void ShowShield()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        timerIsActive = gameObject.activeInHierarchy;
        if (timerIsActive == false)
        {
            timer = 0;
        }
    }


    void TimerManager()
    {
        if (timerIsActive == true)
        {
            timer += Time.deltaTime;
        }

    }
}


