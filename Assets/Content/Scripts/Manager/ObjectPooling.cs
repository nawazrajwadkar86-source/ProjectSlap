using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObjectPooling : MonoBehaviour
{
    #region Singleton

    public static ObjectPooling instance;

    #endregion
    [System.Serializable]
    public class pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    public List<pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    private void Awake()
    {
        instance = this;

        transform.position = Vector3.zero;
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject g = Instantiate(pool.prefab,transform);
                    g.SetActive(false);
                    objectPool.Add(g);
            }
            poolDictionary.Add(pool.name, objectPool);
        }
    }
    public GameObject GetObject(string name,Vector3 position,Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(name))
        {
            Debug.LogWarning("Pool with name " + name + " does not exist");
        }

        List<GameObject> objectPool = poolDictionary[name];

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
