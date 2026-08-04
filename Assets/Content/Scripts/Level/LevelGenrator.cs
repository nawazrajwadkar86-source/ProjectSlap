using System.CodeDom.Compiler;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGenrator : MonoBehaviour
{
    public Transform Player;
    public List<Chunk> chunks;
    private Queue<GameObject> chunksQueue = new Queue<GameObject>();
    public const int chunkLength = 60;
    ChunkPooling objectPooling;
    private int currentGenChunkIndex;
    private int currentCreateIndex = 1;
    private int nextChunkGenCallPos;

    private void Start()
    {
        objectPooling = ChunkPooling.instance;
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
            objectPooling.StoreObject(chunksQueue.Dequeue());

            nextChunkGenCallPos = currentCreateIndex * chunkLength;

            currentChunkIndex.text = "Current Chunk: " + currentCreateIndex.ToString();
            currentCreateIndex++;
        }
    }
    private void GenrateStartingChunk()
    {
        //genrate starting 5 chunks;
        GameObject newChunk = objectPooling.GetObject(poolObject.safe, Vector3.zero, Quaternion.identity);

        chunksQueue.Enqueue(newChunk);
        currentCreateIndex++;
        currentGenChunkIndex++;

        for(int i = 0;i < 8; i++)
        {
            GenerateChunk();
        }

        nextChunkGenCallPos = currentCreateIndex * chunkLength;
        currentCreateIndex++;
    }
    private void GenerateChunk()
    {
        //int RandomChunk = Random.Range(0, chunks.Count);
        int RandomChunk = 0;
        Chunk chunk = chunks[RandomChunk];
        GameObject newChunk = null;
        Vector3 nextChunkPosition = new Vector3(0, 0, currentGenChunkIndex * chunkLength);

        switch (chunk.chunkType)
        {
            case ChunkType.safe:
                newChunk = objectPooling.GetObject(poolObject.safe, nextChunkPosition, Quaternion.identity);
                break;
            case ChunkType.crowd:
                newChunk = objectPooling.GetObject(poolObject.crowd, nextChunkPosition, Quaternion.identity);
                break;
            case ChunkType.hazard:
                newChunk = objectPooling.GetObject(poolObject.hazard, nextChunkPosition, Quaternion.identity);
                break;
            case ChunkType.mixed:
                newChunk = objectPooling.GetObject(poolObject.mixed, nextChunkPosition, Quaternion.identity);
                break;
            case ChunkType.event_:
                newChunk = objectPooling.GetObject(poolObject.event_, nextChunkPosition, Quaternion.identity);
                break;
            case ChunkType.transition:
                newChunk = objectPooling.GetObject(poolObject.transition, nextChunkPosition, Quaternion.identity);
                break;
        }

        chunksQueue.Enqueue(newChunk);
        currentGenChunkIndex++;
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
    transition
}