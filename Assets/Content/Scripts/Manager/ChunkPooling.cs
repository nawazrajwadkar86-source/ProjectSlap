using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ChunkPooling : MonoBehaviour
{
   
    #region Singleton

    public static ChunkPooling instance;

    #endregion
    [System.Serializable]
    public class pool
    {
        public poolObject poolObject;
        public List<GameObject> prefab;
        public int size;
    }

    public List<pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        transform.position = Vector3.zero;
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                for (int j = 0; j < pool.prefab.Count;j++)
                {
                    GameObject g = Instantiate(pool.prefab[j],transform);
                    g.SetActive(false);
                    objectPool.Add(g);
                }
            }
            poolDictionary.Add(pool.poolObject.ToString(), objectPool);
        }
    }
    public GameObject GetObject(poolObject poolObject,Vector3 position,Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolObject.ToString()))
        {
            Debug.LogWarning("Pool with name " + poolObject.ToString() + " does not exist");
        }

        List<GameObject> objectPool = poolDictionary[poolObject.ToString()];

        foreach (GameObject g in objectPool)
        {
            if (!g.activeInHierarchy)
            {
                g.SetActive(true);
                g.transform.position = position;
                g.transform.rotation = rotation;
                return g;
            }
        }
        
        return null;
    }
    public void StoreObject(GameObject enemy)
    {
        enemy.SetActive(false);
    }
    public void StoreObject(GameObject enemy,float delay)
    {
        StartCoroutine(StoreObjectCoroutine(enemy,delay));
    }
    IEnumerator StoreObjectCoroutine(GameObject enemy,float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.SetActive(false);
    }
}
public enum poolObject
{
    safe,
    crowd,
    hazard,
    mixed,
    event_,
    transition
}
