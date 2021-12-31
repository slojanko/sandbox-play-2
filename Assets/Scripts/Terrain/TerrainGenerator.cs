using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noise;

namespace Sandbox { 
	public class TerrainGenerator : MonoBehaviour
	{
		private TerrainProperies properties;
		private Texture2D heightmap;
		private Dictionary<(int, int, int), ChunkData> chunkData;
		private Vector3Int chunkCount;

		private Color32[] heightmapColor;

		void Start()
		{
			properties = TerrainManager.Instance.GetTerrainProperties("base");
			heightmap = TerrainManager.Instance.GetHeightmap("base");
			CreateChunkData();
			//PopulateChunk();
			//BuildChunk();
		}

		void CreateChunkData()
		{
			heightmapColor = heightmap.GetPixels32();

			Vector2Int dimensions = new Vector2Int(heightmap.width, heightmap.height);
			chunkCount = new Vector3Int(
				Mathf.CeilToInt(dimensions.x / properties.textureSampleArea),
				Mathf.CeilToInt(dimensions.y / properties.textureSampleArea),
				Mathf.CeilToInt(256 / properties.textureSampleArea)
			);

			for (var x = 0; x < chunkCount.x; x++)
			{
				for (var y = 0; y < chunkCount.y; y++)
				{
					for (var z = 0; z < chunkCount.z; z++)
					{
						ChunkData chunk = new ChunkData(properties.chunkResolution); 
						chunkData[(x, y, z)] = chunk;

						GameObject chunkController = Instantiate(TerrainManager.Instance.chunkPrefab);
						chunkController.transform.position = new Vector3(x, y, z) * properties.chunkSize;
						chunkController.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * properties.chunkSize;
						chunkController.GetComponent<ChunkController>().Init(chunk);

						PopulateChunkData(chunk, (x, y, z));
					}
				}
			}
		}

		void PopulateChunkData(ChunkData chunk, (int, int, int) index)
		{
			for (int x = 0; x <= properties.chunkResolution; x++)
			{
				for (int y = 0; y <= properties.chunkResolution; y++)
				{
					float u = (index.Item1 + x / properties.chunkResolution) * (float)properties.textureSampleArea;
					float v = (index.Item2 + y / properties.chunkResolution) * (float)properties.textureSampleArea;
					float height = heightmap.GetPixelBilinear(u, v).r;

					for (int z = 0; z <= properties.chunkResolution; z++)
					{
						float zz = (index.Item3 + z / properties.chunkResolution) * (float)properties.textureSampleArea;
						float filled = zz < height ? 1.0f : 0.0f;
						chunk.SetTerrainValue(x, y, z, filled);
					}
				}
			}

			chunk.DispactChanges();
		}

		void PopulateChunk(int x, int y, int z)
		{
            //for (int x = 0; x < properties.chunkResolution; x++)
            //{
            //    for (int y = 0; y < properties.chunkResolution; y++)
            //    {
            //        for (int z = 0; z < properties.chunkResolution; z++)
            //        {
            //            //Vector3 sampleAt = (chunkData.index + new Vector3(x, y, z) / properties.chunkResolution);
            //            //chunkData.terrain[x, y, z] = Perlin.Noise(sampleAt);
            //        }
            //    }
            //}
        }

		void BuildChunk()
		{
			Mesh mesh = new Mesh();
			List<Vector3> meshVertices = new List<Vector3>();
			List<int> meshIndices = new List<int>();

			Vector3[] vertlist = new Vector3[12];
			for (int x = 0; x < properties.chunkResolution - 1; x++)
			{
				for (int y = 0; y < properties.chunkResolution - 1; y++)
				{
					for (int z = 0; z < properties.chunkResolution - 1; z++)
					{
						// Go through each cube inside the terrain (=xyz) and check each vertex belonging to this cube (=i) and build cubeIndex
						int cubeIndex = 0;
						for (int i = 0; i < 8; i++)
						{
							if (SampleTerrain(x, y, z, i) > properties.terrainThreshold)
							{
								cubeIndex |= 1 << i;
							}
						}

						int edges = MarchingCubesLookup.edgeTable[cubeIndex];
						if (edges == 0)
							continue;

						// From cubeIndex get which edges contain vertices of final mesh to separate inside/outside
						if ((edges & 1) != 0)
							vertlist[0] = VertexInterp(x, y, z, 0, 1);
						if ((edges & 2) != 0)
							vertlist[1] = VertexInterp(x, y, z, 1, 2);
						if ((edges & 4) != 0)
							vertlist[2] = VertexInterp(x, y, z, 2, 3);
						if ((edges & 8) != 0)
							vertlist[3] = VertexInterp(x, y, z, 3, 0);
						if ((edges & 16) != 0)
							vertlist[4] = VertexInterp(x, y, z, 4, 5);
						if ((edges & 32) != 0)
							vertlist[5] = VertexInterp(x, y, z, 5, 6);
						if ((edges & 64) != 0)
							vertlist[6] = VertexInterp(x, y, z, 6, 7);
						if ((edges & 128) != 0)
							vertlist[7] = VertexInterp(x, y, z, 7, 4);
						if ((edges & 256) != 0)
							vertlist[8] = VertexInterp(x, y, z, 0, 4);
						if ((edges & 512) != 0)
							vertlist[9] = VertexInterp(x, y, z, 1, 5);
						if ((edges & 1024) != 0)
							vertlist[10] = VertexInterp(x, y, z, 2, 6);
						if ((edges & 2048) != 0)
							vertlist[11] = VertexInterp(x, y, z, 3, 7);

						for (int i = 0; MarchingCubesLookup.triTable[cubeIndex, i] != -1; i += 3)
						{
							meshVertices.Add(new Vector3(x, y, z) + vertlist[MarchingCubesLookup.triTable[cubeIndex, i + 2]]);
							meshVertices.Add(new Vector3(x, y, z) + vertlist[MarchingCubesLookup.triTable[cubeIndex, i + 1]]);
							meshVertices.Add(new Vector3(x, y, z) + vertlist[MarchingCubesLookup.triTable[cubeIndex, i]]);
							meshIndices.Add(meshIndices.Count);
							meshIndices.Add(meshIndices.Count);
							meshIndices.Add(meshIndices.Count);
						}
					}
				}
			}

			mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
			mesh.vertices = meshVertices.ToArray();
			mesh.triangles = meshIndices.ToArray();
			mesh.RecalculateNormals();
			mesh.UploadMeshData(true);
			GetComponent<MeshFilter>().mesh = mesh;
		}

		float SampleTerrain(int x, int y, int z, int vertexIndex)
		{
			Vector3Int vertex = MarchingCubesLookup.vertexIndexToLocalOffset[vertexIndex];
			//return chunkData.terrain[x + vertex.x, y + vertex.y, z + vertex.z];
			return 0.0f;
		}

		Vector3 VertexInterp(int x, int y, int z, int vertexIndex1, int vertexIndex2)
		{
			float v1 = SampleTerrain(x, y, z, vertexIndex1);
			float v2 = SampleTerrain(x, y, z, vertexIndex2);
			Vector3 p1 = MarchingCubesLookup.vertexIndexToLocalOffset[vertexIndex1];
			Vector3 p2 = MarchingCubesLookup.vertexIndexToLocalOffset[vertexIndex2];
			float mu;

			if (Mathf.Abs(properties.terrainThreshold - v1) < 0.00001f)
				return p1;
			if (Mathf.Abs(properties.terrainThreshold - v2) < 0.00001f)
				return p2;
			if (Mathf.Abs(v1 - v2) < 0.00001f)
				return p1;

			mu = Mathf.InverseLerp(v1, v2, properties.terrainThreshold);
			return p1 + mu * (p2 - p1);
		}
	}
}