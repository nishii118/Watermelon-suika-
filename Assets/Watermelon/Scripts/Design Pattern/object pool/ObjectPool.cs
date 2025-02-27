using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //public static ObjectPool instance;
    List<GameObject> pooledObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

     void Awake()
    {
        Init();
        
    }
   
    void Init()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPoolObject()
    {
        if (pooledObjects == null)
        {
            return null;
        }
        for (int i = 0; i < pooledObjects.Count;i++)
        {
            if (pooledObjects[i].activeInHierarchy == false)
            {
                return pooledObjects[i];
            }
        }
        GameObject tmp = Instantiate(objectToPool, transform);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
        amountToPool++;
        return pooledObjects[amountToPool - 1];
    }    

    public GameObject GetObjectToPool()
    {
        return objectToPool;
    }
}
