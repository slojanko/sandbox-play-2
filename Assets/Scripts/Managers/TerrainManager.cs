using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox { 
	public class TerrainManager : ManagerBase<TerrainManager>
	{
		[SerializeField]
		private List<TerrainGeneratorData> data;

		public TerrainGeneratorData GetTerrainGeneratorData(string name)
		{
			return data[0];
		}
	}
}
