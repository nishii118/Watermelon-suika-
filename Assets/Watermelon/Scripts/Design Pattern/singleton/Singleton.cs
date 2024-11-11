using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    [SerializeField] private bool dontDestroyOnLoad;
    public virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = (T)FindObjectOfType(typeof(T));
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}