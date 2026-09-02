using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorGeneratorStairs : MonoBehaviour
{
    public List<ActorPlacement> actorPlacementList;
    private ActorPlacement actorPlacement;
    private ObjectPooling objectPooling;
    public ActorContainer actorContainer;
    public List<TransfomList> spawnPoints = new List<TransfomList>();
    public List<GameObject> spawnedActors = new List<GameObject>();
    public UnityEvent<int> SpawnGates;
    int gateIndex;
    private void OnEnable()
    {
        if(objectPooling != null)
        {
            spawnActors();
        }
        
    }
    private void Start()
    {
        objectPooling = ObjectPooling.instance;
        spawnActors();
    }
    private void OnDisable()
    {
        ClearSpawnedActors();
    }

    private void spawnActors()
    {
        gateIndex = (int)Random.Range(0,3);
        
        SpawnGates?.Invoke(gateIndex);

        actorPlacement = actorPlacementList[gateIndex];

        for(int i = 0; i < actorPlacement.PlacementSlots.Length; i++)
        {
            for(int j = 0; j < actorPlacement.PlacementSlots[i].placmenColumns.Length; j++)
            {
                if(actorPlacement.PlacementSlots[i].placmenColumns[j] == Category.Empty) continue;
                GameObject actor = objectPooling.GetObject(actorContainer.GetObjectName(actorPlacement.PlacementSlots[i].placmenColumns[j]),spawnPoints[i].column[j].position,Quaternion.identity);
                spawnedActors.Add(actor);
            }
        }
        
    }
        private void ClearSpawnedActors()
    {
        foreach (var a in spawnedActors)
        {
            a.SetActive(false);
        }
        spawnedActors.Clear();
    }
}
[System.Serializable]
public class TransfomList
{
    public List<Transform> column = new List<Transform>();
}