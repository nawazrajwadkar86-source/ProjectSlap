using UnityEngine;

[CreateAssetMenu(fileName = "ChunkProperty", menuName = "Scriptable Objects/Chunk/ChunkProperty")]
public class ChunkProperty : ScriptableObject
{
    public string name;
    public ChunkType chunkType;
    public GameObject chunkPrefab;
    public float chunkLength;
    public float YTranslation;
}
