using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sObjectPool;
    [SerializeField] private List<GameObject> pool;
    [SerializeField] private GameObject[] pooledObjects;
    int poolMax;
    int maxNum = 5;

    private void Awake()
    {
        sObjectPool = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolMax = pooledObjects.Length * maxNum;
        pool = new List<GameObject>(poolMax);
        for(int i = 0; i < poolMax; i++)
        {
            for (int j = 0; j < maxNum; j++)
            {
	            GameObject tempObj = (GameObject)Instantiate(pooledObjects[j]);
	            tempObj.SetActive(false);
	            pool.Add(tempObj);
            }
        }
    }

    /*Generates a random index to return a random object
     * If object is already active, search up linearly
     * and return the next available object. If nothing 
     * is found search backwards as a last resort and 
     * return null if absolutely nothing was found.*/
    public GameObject GetPooledObject()
    {

        int objectToGet = Random.Range(0, pooledObjects.Length - 1);

        if (!pool[objectToGet].activeInHierarchy)
            return pool[objectToGet];
        else
        {
	        for(int i = objectToGet + 1; i < pool.Count; i++)
	            if (!pool[i].activeInHierarchy)
	                return pool[i];

            for (int i = objectToGet - 1; i > 0; i--)
                if (!pool[i].activeInHierarchy)
                    return pool[i];
        }
        return null;
    }
}
