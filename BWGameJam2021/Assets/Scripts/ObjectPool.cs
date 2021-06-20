using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sObjectPool;
    List<GameObject> pool;
    GameObject pooledObject;
    int poolMax;

    private void Awake()
    {
        sObjectPool = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        for(int i = 0; i < poolMax; i++)
        {
            GameObject tempObj = (GameObject)Instantiate(pooledObject);
            tempObj.SetActive(false);
            pool.Add(tempObj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }
        return null;
    }
}
