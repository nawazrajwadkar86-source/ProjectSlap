using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ActorGenerator : MonoBehaviour
{
    private ActorsPlacementBluprint actorsPlacementBluprints;
    private ActorPlacement actorPlacement;
    private ObjectPooling objectPooling;
    public ActorContainer actorContainer;
    public SpawnPointsRow[] spawnPoints = new SpawnPointsRow[6];
    private const int laneWidthOffset = 2;
    private const int laneLengthOffset = 10;
    public List<GameObject> spawnedActors = new List<GameObject>();

    private void OnEnable()
    {
        if(objectPooling != null)
        {
            spawnActors();  
        }
    }

    private void Start()
    {
        Initialize();
        spawnActors();
    }
    private void OnDisable()
    {
        ClearSpawnedActors();
    }
        private void Initialize()
    {
        objectPooling = ObjectPooling.instance;

        //Spawn points
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            
            spawnPoints[i].column[0] = new Vector3(-laneWidthOffset, 0, i * laneLengthOffset);
            spawnPoints[i].column[1] = new Vector3(0, 0, i * laneLengthOffset);
            spawnPoints[i].column[2] = new Vector3(laneWidthOffset, 0, i * laneLengthOffset);
        }

        //
        actorsPlacementBluprints = Resources.Load<ActorsPlacementBluprint>("Actor Placement Bluprints/ActorsPlacementBluprint");
        actorPlacement = actorsPlacementBluprints.placementList[Random.Range(0, actorsPlacementBluprints.placementList.Count)];

    }
    private void spawnActors()
    {
        for(int i = 0; i < actorPlacement.PlacementSlots.Length; i++)
        {
            for(int j = 0; j < actorPlacement.PlacementSlots[i].placmenColumns.Length; j++)
            {
                if(actorPlacement.PlacementSlots[i].placmenColumns[j] == Category.Empty) continue;
                GameObject actor = objectPooling.GetObject(actorContainer.GetObjectName(actorPlacement.PlacementSlots[i].placmenColumns[j]),spawnPoints[i].column[j]+transform.position+Vector3.up*0.225f,Quaternion.identity);
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
    private bool GetRandomBool()
    {
        return Random.Range(0, 2) == 1 ? true : false;
    }
    [System.Serializable]
    public class SpawnPointsRow
    {
        public Vector3[] column = new Vector3[3];
    }

}
