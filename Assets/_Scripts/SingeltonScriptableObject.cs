using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingeltonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    public static T instance = null;
    public static T Instance
    {
        get
        {

            if (instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    Debug.Log("No singelton found");
                    return null;
                }
                if (results.Length > 1)
                {
                    Debug.Log("More than one singelton found");
                    return null;
                }
                instance = results[0];
                instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }
            return instance;
        }
    }

}
