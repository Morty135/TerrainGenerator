using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject ChunkPrefab;
    [SerializeField]
    private GameObject TerrainParrent;
    private Dictionary<GameObject, Vector2> Chunks = new Dictionary<GameObject, Vector2>();
    private int ChunkSize = 10;

    //thisNumberShouldntBeHigherThan 10
    [SerializeField]
    private int RenderDistance;

    public Vector2 TerrainGenPlayerPos;
    private Vector2 PreviousPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newChunk = Instantiate(ChunkPrefab, new Vector2(0, 0), Quaternion.identity, TerrainParrent.transform);
        newChunk.GetComponent<Chunk>().ChunkSize = ChunkSize;
        newChunk.name = "Chunk " + newChunk.transform.position.x + "x" + newChunk.transform.position.y;
        Chunks.Add(newChunk, new Vector2(0, 0));

        TerrainGenPlayerPos = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        if (TerrainGenPlayerPos != PreviousPlayerPos)
        {
            PreviousPlayerPos = TerrainGenPlayerPos;
            GenerateChunks(TerrainGenPlayerPos);
        }
    }

    void GenerateChunks(Vector2 PlayerPos)
    {
        foreach (var chunk in Chunks)
        {
            Vector2 chunkPos = chunk.Value;
            if (Mathf.Abs(chunkPos.x - PlayerPos.x) < ChunkSize && Mathf.Abs(chunkPos.y - PlayerPos.y) < ChunkSize)
            {
                // Check if there are empty chunks around the player
                for (int x = -RenderDistance; x <= RenderDistance; x++)
                {
                    for (int y = -RenderDistance; y <= RenderDistance; y++)
                    {
                        Vector2 newChunkPos = new Vector2(chunkPos.x + x * ChunkSize, chunkPos.y + y * ChunkSize);
                        if (!Chunks.ContainsValue(newChunkPos))
                        {
                            // Spawn a new chunk
                            GameObject newChunk = Instantiate(ChunkPrefab, newChunkPos, Quaternion.identity, TerrainParrent.transform);
                            newChunk.GetComponent<Chunk>().ChunkSize = ChunkSize;
                            newChunk.name = "Chunk " + newChunk.transform.position.x + "x" + newChunk.transform.position.y;
                            Chunks.Add(newChunk, newChunkPos);
                        }
                    }
                }
                break;
            }
        }
    }
}
