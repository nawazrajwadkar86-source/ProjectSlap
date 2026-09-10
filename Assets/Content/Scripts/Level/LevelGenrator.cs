using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGenrator : MonoBehaviour
{
    public static LevelGenrator instance;
    public Transform Player;
    public List<Chunk> chunks;
    private Queue<GameObject> chunksObjQueue = new Queue<GameObject>();
    private Queue<ChunkProperty> chunkQueue = new Queue<ChunkProperty>();
    public float previousChunkLength;
    ChunkPooling ChunkPooling;
    //Important
    private float currentChunkbasePos; // Y
    private float nextChunkPosZ;
    private float nextChunkGenCallPos;
    private int currentChunkNum;
    public Action OnchunkUpdate;

    //Elevator spawns
    private int totalFloor = 2;
    private int currentFloor = 1;
    private Vector2Int minMaxNextElevatorIndex = new Vector2Int(7,10);
    private int newElevatorSpawnIndex;

    private const string elevator01_Up = "Elevator01_Up";
    private const string elevator01_Down = "Elevator01_Down";

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        
        ChunkPooling = ChunkPooling.instance;
        GenrateStartingChunk();

        InitializeDebugging();
    }
    private void Update()
    {
        GenrateChunkPerSection();
        //currentChunkText.text = currentChunkNum.ToString();
    }
    private void GenrateChunkPerSection()
    {
        if(Player.position.z > nextChunkGenCallPos)
        {
            GenerateChunk();     

            //initial chunk
            ChunkPooling.StoreObject(chunksObjQueue.Dequeue()); 
            
            //nextChunkGenCallPos += previousChunkLength;
            nextChunkGenCallPos += chunkQueue.Peek().chunkLength;
            chunkQueue.Dequeue();
            
            //currentChunkIndex.text = "Current Chunk: " + currentCreateIndex.ToString();
        }
    }
    private void GenrateStartingChunk()
    {
        //genrate starting 5 chunks;
        GameObject newChunk = ChunkPooling.GetObject(ChunkPooling.GetChunkProperty(ChunkType.safe), Vector3.zero, Quaternion.identity);

        chunksObjQueue.Enqueue(newChunk);

        previousChunkLength = 60;
        nextChunkPosZ = 60;

        for(int i = 0;i < 15; i++)
        {
            GenerateChunk();
        }
        
        nextChunkGenCallPos = 45 + chunkQueue.Peek().chunkLength;

        newElevatorSpawnIndex = newElevatorSpawnIndex = currentChunkNum + UnityEngine.Random.Range(minMaxNextElevatorIndex.x,minMaxNextElevatorIndex.y);
        currentChunkNum ++;
    }
    private void GenerateChunk()
    {
        //int RandomChunk = Random.Range(0, chunks.Count);
        int [] tempIndex = {0,1,3};//
        int RandomChunk = tempIndex[UnityEngine.Random.Range(0,tempIndex.Length)];
        Chunk chunk = chunks[RandomChunk];
        GameObject newChunk;
        ChunkProperty newChunkProperty = null;

        if (currentChunkNum > newElevatorSpawnIndex)
        {
            newChunkProperty = ChunkPooling.GetChunkPropertyByName(selectElevator());
            newElevatorSpawnIndex = currentChunkNum + UnityEngine.Random.Range(minMaxNextElevatorIndex.x,minMaxNextElevatorIndex.y);
        }
        else
        {
            switch (chunk.chunkType)
        {
            case ChunkType.safe:
                newChunkProperty = ChunkPooling.GetChunkProperty(ChunkType.safe);
                break;
            case ChunkType.crowd:
                newChunkProperty = ChunkPooling.GetChunkProperty(ChunkType.crowd);
                break;
            case ChunkType.hazard:
                newChunkProperty = ChunkPooling.GetChunkProperty(ChunkType.hazard);
                break;
            case ChunkType.mixed:
                newChunkProperty = ChunkPooling.GetChunkProperty(ChunkType.mixed);
                break;
            case ChunkType.event_:
                newChunkProperty = ChunkPooling.GetChunkProperty(ChunkType.event_);
                break;
            case ChunkType.transition:
                newChunkProperty = ChunkPooling.GetChunkProperty(ChunkType.transition);
                break;
        }
        }

        
        

        Vector3 nextChunkPosition = new Vector3(0, currentChunkbasePos, nextChunkPosZ);
        newChunk = ChunkPooling.GetObject(newChunkProperty, nextChunkPosition, Quaternion.identity);

        currentChunkbasePos += newChunkProperty.YTranslation;
        previousChunkLength = newChunkProperty.chunkLength;
        nextChunkPosZ += newChunkProperty.chunkLength;

        currentChunkNum++;

        chunkQueue.Enqueue(newChunkProperty);

        chunksObjQueue.Enqueue(newChunk);
    
        OnchunkUpdate?.Invoke();
    }
    private string selectElevator()
    {
        string value = "";
        if(currentFloor == 1)
        {
            if( UnityEngine.Random.Range(0.0f,1.0f) > 0.5f)
            {
                value = elevator01_Up;
                currentFloor = 2;
            }
            else
            {
                value = elevator01_Down;
                currentFloor = 0;
            }
        }else if(currentFloor == 0)
        {
            value = elevator01_Up;
            currentFloor = 1;
        }else if (currentFloor == 2)
        {
            value = elevator01_Down;
            currentFloor = 1;
        }

        return value;
    }


        //Debugging
    private Canvas canvas;
    private TextMeshProUGUI currentChunkText;
    private void InitializeDebugging()
    {
        canvas = FindAnyObjectByType<Canvas>();
        currentChunkText = CreateText("Current Chunk: 0", new Vector2(-Screen.width / 2 - 100, Screen.height / 2 + 50));
    }

    private TextMeshProUGUI CreateText(string text, Vector2 position)
    {
        GameObject textObj = new GameObject("DebugText");
        textObj.transform.SetParent(canvas.transform);
        textObj.transform.localPosition = position;

        TextMeshProUGUI textMesh = textObj.AddComponent<TextMeshProUGUI>();
        textMesh.text = text;
        textMesh.color = Color.red;
        textMesh.fontSize = 24;
        return textMesh;
    }
}
[System.Serializable]
public class Chunk
{
    public ChunkType chunkType;
}

public enum ChunkType
{
    safe,
    crowd,
    hazard,
    mixed,
    event_,
    transition,   
}