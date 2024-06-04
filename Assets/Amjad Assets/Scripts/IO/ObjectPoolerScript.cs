using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerScript : MonoBehaviour {
	public static ObjectPoolerScript current;

	public GameObject[] pooledObject;
	public int pooledAmount = 10;
	public bool willGrow = true;
	private int ir = 0;
	private List<GameObject> pooledObjects; 

	void Awake()
	{
		current = this;

		}
	void Start () {
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++)
		{
			ir = Random.Range (0, pooledObject.Length);
			GameObject obj = (GameObject)Instantiate(pooledObject[ir]);
			obj.SetActive(false);
			pooledObjects.Add (obj); 
		}
	}
	public GameObject GetPooledObject(){
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		if (willGrow) {
			ir = Random.Range (0, pooledObject.Length);
			GameObject obj = (GameObject)Instantiate(pooledObject[ir]);
			pooledObjects.Add (obj); 
			return obj;
				}
		return null;

	}
}
