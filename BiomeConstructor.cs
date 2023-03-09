using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomeConstructor
{
    public string BiomeName;
    public GameObject[] BiomeTiles;
    public GameObject[] BiomeFoliage;
    //mustBeBetween 0 and 1
    public float AmountOfFoliageToSpawn;
    public float TileDistribution;
    public float FoliageDistribution;
}
