﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pool : MonoBehaviour
{	
	public GameObject prefab;
	public int maxCount = 100;
	public int prepareCount = 0;
	[SerializeField]
	private int interval = 1;
	private List<GameObject> pooledObjectList = new List<GameObject> ();
	private static GameObject poolAttachedObject = null;
	
	void OnEnable ()
	{
		if (interval > 0)
			StartCoroutine (RemoveObjectCheck ());
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	public void OnDestroy ()
	{
		if (poolAttachedObject == null)
			return;
		
		if (poolAttachedObject.GetComponents<Pool> ().Length == 1) {
			poolAttachedObject = null;
		}
		foreach (var obj in pooledObjectList) {
			Destroy (obj);
		}
		pooledObjectList.Clear ();
	}
	
	public int Interval
	{
		get{
			return interval;
		}
		set{
			if( interval != value)
			{
				interval = value;
				
				StopAllCoroutines();
				if( interval > 0)
					StartCoroutine(RemoveObjectCheck ());
			}
		}
	}

	/*
	public GameObject GetInstance ()
	{
		return GetInstance(transform);
	}
	*/

	public GameObject GetInstance (Transform parentInfo)
	{
		pooledObjectList.RemoveAll( (obj) => obj == null);
		
		foreach (GameObject obj in pooledObjectList) {
			if (obj.activeSelf == false) {
				obj.SetActive (true);
				return obj;	
			}
		}
		//if (pooledObjectList.Count < maxCount) {
			GameObject obj2 = (GameObject)GameObject.Instantiate (prefab);
			obj2.SetActive (true);
			obj2.transform.parent = parentInfo;
			pooledObjectList.Add (obj2);
			return obj2;
		//}
		//return null;
	}
	
	IEnumerator RemoveObjectCheck ()
	{
		while (true) {
			RemoveObject (prepareCount);
			yield return new WaitForSeconds (interval);
		}
	}
	
	public void RemoveObject (int max)
	{
		if (pooledObjectList.Count > max) {
			int needRemoveCount = pooledObjectList.Count - max;
			foreach (GameObject obj in pooledObjectList.ToArray()) {
				if (needRemoveCount == 0) {
					break;
				}
				if (obj.activeSelf == false) {
					pooledObjectList.Remove (obj);
					Destroy (obj);
					needRemoveCount --;
				}
			}
		}
	}

	public GameObject[] SaveObjectList()
	{
		var objects = from item in  pooledObjectList
				where item.activeSelf == true
				select item;

		return objects.ToArray();
	}

	
	public static Pool GetObjectPool (GameObject obj)
	{
		if (poolAttachedObject == null) {
			poolAttachedObject = GameObject.Find ("ObjectPool");
			if (poolAttachedObject == null) {
				poolAttachedObject = new GameObject ("ObjectPool");
			}
		}
		foreach (var pool in poolAttachedObject.GetComponents<Pool>()) {
			if (pool.prefab == obj) {
				return pool;
			}
		}
		
		/*
		Pool[] poolObjects = FindObjectsOfType(typeof(Pool)) as Pool[];
		
		foreach(var pool in poolObjects){
			if (pool.prefab == obj) {
				return pool;
			}
		}
		*/

		foreach (var pool in FindObjectsOfType<Pool>()) {
			if (pool.prefab == obj) {
				return pool;
			}
		}

		
		var newPool = poolAttachedObject.AddComponent<Pool> ();
		newPool.prefab = obj;
		return newPool;
	}
}