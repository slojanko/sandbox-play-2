using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainGeneratorData", order = 1)]
public class TerrainGeneratorData : ScriptableObject
{
	public int chunkResolution = 8;
	public float chunkSize = 4.0f;
	public float chunkNoiseArea = 0.1f;
	public float terrainThreshold = 0.0f;
}
