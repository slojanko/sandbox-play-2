using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox { 
	enum TerrainType { Heightmap, Volume };
	enum TerrainOperation { Add, Subtract};

	[System.Serializable]
	public struct TerrainLayer
	{
		TerrainType type;
		TerrainOperation operation;
		float frequency;
		float amplitude;
	}

	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainGeneratorData", order = 1)]
	public class TerrainGeneratorData : ScriptableObject
	{
		public int chunkResolution = 8;
		public float chunkSize = 4.0f;
		public float terrainThreshold = 0.0f;

		public List<TerrainLayer> layers;
	}
}