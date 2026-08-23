using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class LevelGenrator : MonoBehaviour
{
    public Transform Player;
    public List<Chunk> chunks;
    private Queue<GameObject> chunksQueue = new Queue<GameObject>();
    public float previousChunkLength;
    ChunkPooling ChunkPooling;

    //Important
    private float currentChunkbasePos; // Y
    private float nextChunkPosZ;
    
    private float nextChunkGenCallPos;

    private void Start()
    {
        ChunkPooling = ChunkPooling.instance;
        GenrateStartingChunk();

        InitializeDebugging();
    }
    private void Update()
    {
        GenrateChunkPerSection();
    }
    private void GenrateChunkPerSection()
    {
        if(Player.position.z > nextChunkGenCallPos)
        {
            GenerateChunk();     

            //initial chunk
            ChunkPooling.StoreObject(chunksQueue.Dequeue());

            nextChunkGenCallPos += previousChunkLength;

            //currentChunkIndex.text = "Current Chunk: " + currentCreateIndex.ToString();
        }
    }
    private void GenrateStartingChunk()
    {
        //genrate starting 5 chunks;
        GameObject newChunk = ChunkPooling.GetObject(ChunkPooling.GetChunkProperty(ChunkType.safe), Vector3.zero, Quaternion.identity);

        chunksQueue.Enqueue(newChunk);

        previousChunkLength = 60;
        nextChunkPosZ = 60;

        for(int i = 0;i < 15; i++)
        {
            GenerateChunk();
        }

        nextChunkGenCallPos = 360;
    }
    private void GenerateChunk()
    {
        //int RandomChunk = Random.Range(0, chunks.Count);
        int [] tempIndex = {0,1,3,5};
        int RandomChunk = tempIndex[Random.Range(0,tempIndex.Length)];
        Chunk chunk = chunks[RandomChunk];
        GameObject newChunk;
        ChunkProperty newChunkProperty = null;

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
        

        Vector3 nextChunkPosition = new Vector3(0, currentChunkbasePos, nextChunkPosZ);
        newChunk = ChunkPooling.GetObject(newChunkProperty, nextChunkPosition, Quaternion.identity);

        currentChunkbasePos += newChunkProperty.YTranslation;
        previousChunkLength = newChunkProperty.chunkLength;
        nextChunkPosZ += newChunkProperty.chunkLength;

        chunksQueue.Enqueue(newChunk);
    
    }

        //Debugging
    private Canvas canvas;
    private TextMeshProUGUI currentChunkIndex;
    private void InitializeDebugging()
    {
        canvas = FindAnyObjectByType<Canvas>();
        currentChunkIndex = CreateText("Current Chunk: 0", new Vector2(-Screen.width / 2 + 100, Screen.height / 2 - 50));
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