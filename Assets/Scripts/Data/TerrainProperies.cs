using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox { 
	[CreateAssetMenu(fileName = "Properties", order = 1)]
	public class TerrainProperies : ScriptableObject
	{
		[Header("Chunk")]
		public int chunkResolution = 8;
		public float chunkSize = 4.0f;

		[Header("Texture")]
		public float textureSampleArea = 16;

		[Header("Threshold")]
		public float terrainThreshold = 0.0f;
	}
}