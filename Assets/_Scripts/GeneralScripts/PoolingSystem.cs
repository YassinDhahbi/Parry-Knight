using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    private List<Projectile> listOfProjectiles;

    public Projectile GetProjectile()
    {
        foreach (var item in listOfProjectiles)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                item.gameObject.SetActive(true);
                item.transform.eulerAngles = Vector3.zero;
                return item;
            }
        }
        return null;
    }
}