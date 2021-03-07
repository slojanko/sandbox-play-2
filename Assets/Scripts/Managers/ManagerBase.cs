using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : Component
{
	public static T Instance
	{
		get
		{
			if (cachedInstance == null)
			{
				cachedInstance = FindObjectOfType<T>();
				if (cachedInstance == null)
				{
					Debug.LogError(typeof(T).Name + " instance is missing!");
				}
			}

			return cachedInstance;
		}
	}

	private static T cachedInstance = null;

	protected virtual void Awake()
	{
		if (cachedInstance == null)
		{
			cachedInstance = GetComponent<T>();
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Debug.LogError("Duplicate " + typeof(T).Name + " found!");
			Destroy(this.gameObject);
		}
	}
}
