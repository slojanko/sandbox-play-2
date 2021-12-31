using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox { 
	public class ChunkData
	{
		public delegate void DataChanged();

		public DataChanged dataChanged;
		private float[,,] terrain;

		public ChunkData(int resolution)
		{
			terrain = new float[resolution,resolution,resolution];
		}

        //~ChunkData()
        //{
        //    foreach (System.Delegate d in dataChanged.GetInvocationList())
        //    {
        //        dataChanged -= (DataChanged)d;
        //    }
        //}

        public void SetTerrainValue(int x, int y, int z, float value)
        {
			terrain[x, y, z] = value;
        }

		public void DispactChanges()
        {
			dataChanged();
        }
	}
}