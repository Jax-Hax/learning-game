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
    //[HideInInspector]
    //public List<GameObject> pooledObjects;
    [HideInInspector]
    public List<GameObject> redObjects;
    [HideInInspector]
    public List<GameObject> blueObjects;
    [HideInInspector]
    public List<GameObject> greenObjects;
    [HideInInspector]
    public List<GameObject> yellowObjects;
    [HideInInspector]
    public List<GameObject> pinkObjects;
    [HideInInspector]
    public List<GameObject> blackObjects;
    [HideInInspector]
    public List<GameObject> whiteObjects;
    [HideInInspector]
    public List<GameObject> orangeObjects;
    [HideInInspector]
    public List<GameObject> purpleObjects;
    [HideInInspector]
    public List<GameObject> rainbowObjects;
    [HideInInspector]
    public List<GameObject> ceramicObjects;
    [HideInInspector]
    public List<GameObject> leadObjects;
    [HideInInspector]
    public List<GameObject> redVObjects;
    [HideInInspector]
    public List<GameObject> blueVObjects;
    [HideInInspector]
    public List<GameObject> greenVObjects;
    [HideInInspector]
    public List<GameObject> yellowVObjects;
    [HideInInspector]
    public List<GameObject> pinkVObjects;
    [HideInInspector]
    public List<GameObject> mushroomObjects;
    public List<ObjectPoolItem> itemsToPool;
    //private string objectTag;
    public Transform instantiatedParent;
    private int index;
    void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        /*pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }*/
        index = 0;
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if(index == 0)
            {
                redObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    redObjects.Add(obj);
                }
            }
            else if (index == 1)
            {
                blueObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    blueObjects.Add(obj);
                }
            }
            else if (index == 2)
            {
                greenObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    greenObjects.Add(obj);
                }
            }
            else if (index == 3)
            {
                yellowObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    yellowObjects.Add(obj);
                }
            }
            else if (index == 4)
            {
                pinkObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    pinkObjects.Add(obj);
                }
            }
            else if (index == 5)
            {
                blackObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    blackObjects.Add(obj);
                }
            }
            else if (index == 6)
            {
                whiteObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    whiteObjects.Add(obj);
                }
            }
            else if (index == 7)
            {
                orangeObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    orangeObjects.Add(obj);
                }
            }
            else if (index == 8)
            {
                purpleObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    purpleObjects.Add(obj);
                }
            }
            else if (index == 9)
            {
                rainbowObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    rainbowObjects.Add(obj);
                }
            }
            else if (index == 10)
            {
                ceramicObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    ceramicObjects.Add(obj);
                }
            }
            else if (index == 11)
            {
                leadObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    blueObjects.Add(obj);
                }
            }
            else if (index == 12)
            {
                redVObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    redVObjects.Add(obj);
                }
            }
            else if (index == 13)
            {
                blueVObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    blueVObjects.Add(obj);
                }
            }
            else if (index == 14)
            {
                greenVObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    greenVObjects.Add(obj);
                }
            }
            else if (index == 15)
            {
                yellowVObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    yellowVObjects.Add(obj);
                }
            }
            else if (index == 16)
            {
                pinkVObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    pinkVObjects.Add(obj);
                }
            }
            else if (index == 17)
            {
                mushroomObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, instantiatedParent);
                    obj.SetActive(false);
                    mushroomObjects.Add(obj);
                }
            }
            index++;
        }
    }
    public GameObject GetPooledObject(int tagNumber)
    {
        if(tagNumber == 0)
        {
            for (int i = 0; i < redObjects.Count; i++)
            {
                if (!redObjects[i].activeInHierarchy)
                {
                    return redObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            redObjects.Add(obj);
            return obj;
        }
        else if(tagNumber == 1)
        {
            for (int i = 0; i < blueObjects.Count; i++)
            {
                if (!blueObjects[i].activeInHierarchy)
                {
                    return blueObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            blueObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 2)
        {
            for (int i = 0; i < greenObjects.Count; i++)
            {
                if (!greenObjects[i].activeInHierarchy)
                {
                    return greenObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            greenObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 3)
        {
            for (int i = 0; i < yellowObjects.Count; i++)
            {
                if (!yellowObjects[i].activeInHierarchy)
                {
                    return yellowObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            yellowObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 4)
        {
            for (int i = 0; i < pinkObjects.Count; i++)
            {
                if (!pinkObjects[i].activeInHierarchy)
                {
                    return pinkObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            pinkObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 5)
        {
            for (int i = 0; i < blackObjects.Count; i++)
            {
                if (!blackObjects[i].activeInHierarchy)
                {
                    return blackObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            blackObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 6)
        {
            for (int i = 0; i < whiteObjects.Count; i++)
            {
                if (!whiteObjects[i].activeInHierarchy)
                {
                    return whiteObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            whiteObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 7)
        {
            for (int i = 0; i < orangeObjects.Count; i++)
            {
                if (!orangeObjects[i].activeInHierarchy)
                {
                    return orangeObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            orangeObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 8)
        {
            for (int i = 0; i < purpleObjects.Count; i++)
            {
                if (!purpleObjects[i].activeInHierarchy)
                {
                    return purpleObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            purpleObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 9)
        {
            for (int i = 0; i < rainbowObjects.Count; i++)
            {
                if (!rainbowObjects[i].activeInHierarchy)
                {
                    return rainbowObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            rainbowObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 10)
        {
            for (int i = 0; i < ceramicObjects.Count; i++)
            {
                if (!ceramicObjects[i].activeInHierarchy)
                {
                    return ceramicObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            ceramicObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 11)
        {
            for (int i = 0; i < leadObjects.Count; i++)
            {
                if (!leadObjects[i].activeInHierarchy)
                {
                    return leadObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            leadObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 12)
        {
            for (int i = 0; i < redVObjects.Count; i++)
            {
                if (!redVObjects[i].activeInHierarchy)
                {
                    return redVObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            redVObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 13)
        {
            for (int i = 0; i < blueVObjects.Count; i++)
            {
                if (!blueVObjects[i].activeInHierarchy)
                {
                    return blueVObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            blueVObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 14)
        {
            for (int i = 0; i < greenVObjects.Count; i++)
            {
                if (!greenVObjects[i].activeInHierarchy)
                {
                    return greenVObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            greenVObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 15)
        {
            for (int i = 0; i < yellowVObjects.Count; i++)
            {
                if (!yellowVObjects[i].activeInHierarchy)
                {
                    return yellowVObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            yellowVObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 16)
        {
            for (int i = 0; i < pinkVObjects.Count; i++)
            {
                if (!pinkVObjects[i].activeInHierarchy)
                {
                    return pinkVObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            pinkVObjects.Add(obj);
            return obj;
        }
        else if (tagNumber == 17)
        {
            for (int i = 0; i < mushroomObjects.Count; i++)
            {
                if (!mushroomObjects[i].activeInHierarchy)
                {
                    return mushroomObjects[i];
                }
            }
            ObjectPoolItem item = itemsToPool[tagNumber];
            GameObject obj = Instantiate(item.objectToPool);
            obj.SetActive(false);
            mushroomObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
