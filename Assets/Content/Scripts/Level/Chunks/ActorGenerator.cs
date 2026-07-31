using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ActorGenerator : MonoBehaviour
{
    private ObjectPooling objectPooling;
    public ActorContainer actorContainer;
    public List<SpawnPointsRow> spawnPoints;
    private const int laneWidthOffset = 2;  
    private const int laneLengthOffset = 10;  
    //Doors
    //NPC
    //Boosters
    //Traps
    public List<GameObject> spawnedActors = new List<GameObject>();
    private void Awake()
    {
        objectPooling = ObjectPooling.instance;
    }
    private void Start()
    {
        InitializeSpawnPoints();
        spawnActors();
    }
    private void OnDisable()
    {
        ClearSpawnedActors();
    }
    private void spawnActors()
    {
        //Door 
        if (true||GetRandomBool())
        {
            GameObject o = actorContainer.GetRandomObject(Category.Door,transform.position+spawnPoints[6].b,Quaternion.identity);
            spawnedActors.Add(o);
        }
    }
    private void InitializeSpawnPoints()
    {
        for(int i = 0;i < spawnPoints.Count;i++)
        {
            spawnPoints[i].a = new Vector3(-laneWidthOffset,0,i*laneLengthOffset); 
            spawnPoints[i].b = new Vector3(0,0,i*laneLengthOffset); 
            spawnPoints[i].c = new Vector3(laneWidthOffset,0,i*laneLengthOffset); 
        }
    }
    private void ClearSpawnedActors()
    {
        foreach(var a in spawnedActors)
        {
            if(objectPooling != null)objectPooling.StoreObject(a);
        }
        spawnedActors.Clear();
    }
    private bool GetRandomBool()
    {
        return Random.Range(0,2) == 1?true:false;
    }
    [System.Serializable]
    public class SpawnPointsRow
    {
        public Vector3 a,b,c;
    }

}
