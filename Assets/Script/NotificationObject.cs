using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class NotificationObject<T> :  UnityEngine.Events.UnityEvent<T>
{
	private T data;
	
	public NotificationObject(){}
	public NotificationObject (T t)
	{
		Value = t;
	}
	
	public T Value {
		get {
			return data;
		}
		set {
			data = value;
			Invoke(data);
		}
	}
	
	public void Dispose()
	{
		RemoveAllListeners();
	}
}

[System.Serializable]
public class ScoreObject<T1, T2>: UnityEngine.Events.UnityEvent<T1, T2>
{

	/*
	private int scoreData;
	private Vector3 positionData;

	public ScoreObject(){}
	public ScoreObject(int score, Vector3 pos){

	}
	*/

	public void Dispose()
	{
		RemoveAllListeners();
	}
}




