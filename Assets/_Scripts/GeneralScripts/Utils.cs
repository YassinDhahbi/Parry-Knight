using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UtilityStorer")]
public class Utils : ScriptableObject
{
    public void DeactivateObject(GameObject targetObject)
    {
        targetObject.SetActive(false);
    }
}