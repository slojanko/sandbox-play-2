using UnityEngine;

public static class RandomUtils
{
	private static void ShuffleArray<T>(T[] array)
	{
		int n = array.Length;
		while (n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			T value = array[k];
			array[k] = array[n];
			array[n] = value;
		}
	}
}
