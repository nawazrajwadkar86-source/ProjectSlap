using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ChunkPooling : MonoBehaviour
{
    #region Singleton
    public static ChunkPooling instance;
    #endregion
    public List<pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    private void Awake()
    {
        instance = this;

        transform.position = Vector3.zero;
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (pool pool in pools)
        {
            for (int j = 0; j < pool.chunkPropertiesList.Count; j++)
            {
                List<GameObject> objectPool = new List<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject g = Instantiate(pool.chunkPropertiesList[j].chunkPrefab, transform);
                    g.SetActive(false);
                    objectPool.Add(g);
                    
                }
                poolDictionary.Add(pool.chunkPropertiesList[j].name, objectPool);

            }
        }
    }
    public GameObject GetObject(ChunkProperty chunkProperty, Vector3 position, Quaternion rotation)
    {

        List<GameObject> objectPool = poolDictionary[chunkProperty.name];

        foreach (var g in objectPool)
        {
            if (!g.activeInHierarchy)
            {
                g.transform.position = position;
                g.transform.rotation = rotation;
                g.SetActive(true);
                return g;
            }
        }
        return null;
    }
    public ChunkProperty GetChunkProperty(ChunkType ChunkType)
    {
        ChunkProperty newChunkProperty = null;
        if (!poolDictionary.ContainsKey(ChunkType.ToString()))
        {
            Debug.LogWarning("Pool with name " + ChunkType.ToString() + " does not exist");
        }

        foreach (var p in pools)
        {
            if (p.ChunkType == ChunkType)
            {
                Debug.Log(p.chunkPropertiesList.Count);
                newChunkProperty = p.chunkPropertiesList[Random.Range(0, p.chunkPropertiesList.Count)];
            }
        }

        if (newChunkProperty == null)
        {
            Debug.LogError("chunk property null");
        }
        return newChunkProperty;
    }
    public void StoreObject(GameObject enemy)
    {
        enemy.SetActive(false);
    }
    public void StoreObject(GameObject enemy, float delay)
    {
        StartCoroutine(StoreObjectCoroutine(enemy, delay));
    }
    IEnumerator StoreObjectCoroutine(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.SetActive(false);
    }

    [System.Serializable]
    public class pool
    {
        public ChunkType ChunkType;
        public List<ChunkProperty> chunkPropertiesList = new List<ChunkProperty>();
        public int size;
    }

}

