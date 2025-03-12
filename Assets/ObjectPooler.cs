using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<GameObject> curentPlatorm;

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    public static ObjectPooler Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            curentPlatorm = objectPool;
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position)
    {

        List<GameObject> objectPool = poolDictionary[tag];
        
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = position;
                return obj;
            }
        }
        return null;
    }
}
