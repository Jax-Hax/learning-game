using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
}
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    [HideInInspector]
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;
    private string objectTag;
    public Transform instantiatedParent;
    void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
    public GameObject GetPooledObject(int tagNumber)
    {
        if(tagNumber == 0)
        {
            objectTag = "red";
        }
        else if(tagNumber == 1)
        {
            objectTag = "blue";
        }
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == objectTag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == objectTag)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }
}
