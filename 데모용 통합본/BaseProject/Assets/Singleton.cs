using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T)); 
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            //DontDestroyOnLoad(instance);
            return instance;
        }
    }

    private void Awake()
    {
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

}

/*

private static T instance;
public static T Instance
{
    get
    {
        if (instance == null)
        {
            var obj = FindObjectOfType<T>();
            if (obj != null)
            {
                instance = obj;
            }
            else
            {
                var newSingleton = new GameObject("Singleton Class").AddComponent<T>();
                instance = newSingleton;
            }
        }
        return instance;
    }
    private set
    {
        instance = value;
    }
}
private void Awake()
{
    var objs = FindObjectsOfType<T>();
    if (objs.Length != 1)
    {
        Destroy(gameObject);
        return;

    }
    DontDestroyOnLoad(gameObject);
}*/