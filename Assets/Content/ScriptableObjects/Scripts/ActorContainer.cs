using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actors", menuName = "Scriptable Objects/Actors/ActorsContainer")]
public class ActorContainer : ScriptableObject
{
    public List<ActorsData> container;

    public List<GameObject> GetObjectList(Category category)
    {
        List<GameObject> objList = null;

        foreach(var a in container)
        {
            if(a.Category == category)
            {
                foreach(var name in a.PrefabsName)
                {
                    objList.Add(ObjectPooling.instance.GetObject(name,Vector3.zero,Quaternion.identity));
                }
                break;
            }
        }

        return objList;
    }
    public GameObject GetObject(Category category)
    {
        GameObject obj = null;

        foreach(var a in container)
        {
            if(a.Category == category)
            {
                obj = ObjectPooling.instance.GetObject(a.PrefabsName[0],Vector3.zero,Quaternion.identity);
                break;
            }
        }
        return obj;
    }
    public GameObject GetObject(Category category,Vector3 position,Quaternion rotation)
    {
        GameObject obj = null;

        foreach(var a in container)
        {
            if(a.Category == category)
            {
                obj = ObjectPooling.instance.GetObject(a.PrefabsName[0],position,rotation);
                break;
            }
        }
        return obj;
    }
    public GameObject GetRandomObject(Category category)
    {
        GameObject obj = null;

        foreach(var a in container)
        {
            if(a.Category == category)
            {
                obj = ObjectPooling.instance.GetObject(a.PrefabsName[Random.Range(0,a.PrefabsName.Count)],Vector3.zero,Quaternion.identity);
                break;
            }
        }
        return obj;
    }
    
    public GameObject GetRandomObject(Category category,Vector3 position,Quaternion rotation)
    {
        GameObject obj = null;

        foreach(var a in container)
        {
            if(a.Category == category)
            {
                obj = ObjectPooling.instance.GetObject(a.PrefabsName[Random.Range(0,a.PrefabsName.Count)],position,rotation);
                break;
            }
        }
        return obj;
    }
}
[System.Serializable]
public class ActorsData
{
    public Category Category;
    public List<string>PrefabsName;
}
public enum Category
{
    Empty,
    Door,
    coins,
    coins_DiagonalRight,
    coins_DiagonalLeft,
    Enemy,
    Enemies_Column,
    Enemies_DiagonalRight,
    Enemies_DiagonalLeft,
    CoinEnemies_Column,
    CoinEnemies_DiagonalRight,
    CoinEnemies_DiagonalLeft,
    Booster,
    Traps,
    NPC
}
