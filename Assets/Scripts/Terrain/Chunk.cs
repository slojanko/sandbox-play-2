using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
	public Vector3 index;
	public float[,,] terrain;

	public Chunk(Vector3 index, int resolution)
	{
		this.index = index;
		terrain = new float[resolution,resolution,resolution];
	}
}
