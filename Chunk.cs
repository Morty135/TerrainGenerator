using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [HideInInspector]
    public int ChunkSize;

    public int PerlinOffset;
    public float Freq;
    public float BiomeFreq;

    private int BiomeIndex;
    public BiomeConstructor[] Biomes;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                //BiomeCreator
                BiomeIndex = PerlinOffset + Mathf.RoundToInt(Mathf.Clamp(Biomes.Length * 0.5f * (
                    Mathf.PerlinNoise((this.transform.position.x + x) * BiomeFreq, (this.transform.position.y + y) * BiomeFreq)), 0, Biomes.Length - 1));

                //TileSpawner
                //OctavedPerlinToIntToMakeTileSpawnIndex
                int TileToSpawn = PerlinOffset + Mathf.RoundToInt(Mathf.Clamp(Biomes[BiomeIndex].BiomeTiles.Length * Biomes[BiomeIndex].TileDistribution * (
                    Mathf.PerlinNoise((this.transform.position.x + x) * Freq * 0.5f, (this.transform.position.y + y) * Freq * 0.5f)
                    +  0.5f * Mathf.PerlinNoise((this.transform.position.x + x)  * Freq * 0.5f,(this.transform.position.y + y) * Freq * 0.5f)
                    + 0.25f * Mathf.PerlinNoise((this.transform.position.x + x)  * Freq * 0.25f, (this.transform.position.y + y) * Freq * 0.25f)), 0, Biomes[BiomeIndex].BiomeTiles.Length-1));

                Vector3Int SpawnPos = new Vector3Int(x + Mathf.RoundToInt(this.transform.position.x) ,y + Mathf.RoundToInt(this.transform.position.y), 0);
                Instantiate(Biomes[BiomeIndex].BiomeTiles[TileToSpawn] ,SpawnPos, Quaternion.identity, this.gameObject.transform);



                //FoliageSpown
                int FoliageToSpawn = PerlinOffset + Mathf.RoundToInt(Mathf.Clamp(Biomes[BiomeIndex].BiomeFoliage.Length * Biomes[BiomeIndex].FoliageDistribution * (
                    Mathf.PerlinNoise((this.transform.position.x + x) * Freq * 0.5f, (this.transform.position.y + y) * Freq * 0.5f)), 0, Biomes[BiomeIndex].BiomeFoliage.Length - 1));

                float CanSpawn = Mathf.Clamp(Mathf.PerlinNoise((this.transform.position.x + x) * 1.5f, (this.transform.position.y + y) * 1.5f),0,1);

                if(CanSpawn > Biomes[BiomeIndex].AmountOfFoliageToSpawn)
                {
                    Instantiate(Biomes[BiomeIndex].BiomeFoliage[FoliageToSpawn], SpawnPos, Quaternion.identity, this.gameObject.transform);
                }
            }
        }
    }
}
