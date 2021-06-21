using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameState gs = GameState.Normal;
    public static int num = 0;
    [SerializeField] GameObject spawnPoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPooledObject", 1f, 3f);
    }

    // Update is called once per frame

    void SpawnPooledObject()
    {
        GameObject obj = ObjectPool.sObjectPool.GetPooledObject();
        if (obj)
        {
            obj.transform.position = spawnPoint.transform.position;
            obj.SetActive(true);
        }
        else
            return;
    }

    void Update()
    {
        
    }

}
