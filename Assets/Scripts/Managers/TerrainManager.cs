using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox { 
	public class TerrainManager : ManagerBase<TerrainManager>
	{
		[SerializeField]
		private List<TerrainProperies> properties;
		[SerializeField]
		private List<Texture2D> heightmap;
		[SerializeField]
		public GameObject chunkPrefab { get; private set; }

		public TerrainProperies GetTerrainProperties(string name)
		{
			return properties[0];
		}

		public Texture2D GetHeightmap(string name)
		{
			return heightmap[0];
		}
	}
}
