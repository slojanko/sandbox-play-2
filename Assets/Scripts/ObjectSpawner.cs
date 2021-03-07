using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Vector3 _origin;
    [SerializeField] private Vector3Int _amount;
    [SerializeField] private Vector3 _spacing;

    void Start()
    {
	    Spawn();
    }

    private void Spawn()
    {
	    Vector3Int half_amount = _amount / 2;
	    half_amount.y = 0;
		Vector3 start = _origin - Vector3.Scale(half_amount, _spacing);
		Debug.Log(start);
	    for (int x = 0; x < _amount.x; x++)
	    {
		    for (int y = 0; y < _amount.y; y++)
		    {
			    for (int z = 0; z < _amount.z; z++) 
			    { 
				    Vector3 position = start + Vector3.Scale(_spacing, new Vector3(x, y, z));
					position += Vector3.one * Random.Range(0.0f, 0.01f);
					Instantiate(_object, position, Quaternion.identity, transform);
			    }
		    }
	    }
    }
}
