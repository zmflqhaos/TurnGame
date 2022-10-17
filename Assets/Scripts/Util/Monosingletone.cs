using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shutingDown = false;
    private static object locker = new object();

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (shutingDown)
            {
                Debug.LogWarning("[Instance] Instance" + typeof(T) + "is already destroyed. Retruning null.");
            }

            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                }
            }
            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        shutingDown = true;
    }

    private void OnDestroy()
    {
        shutingDown = true;
    }
}

