using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterManager : MonoBehaviour 
{	
	public string gameObjName { get{return gameObject.name;} }
	private int _thisCharScore;
	public int thisCharScore
	{
		get{ return _thisCharScore;}
		set{ _thisCharScore = value;}
	}

	void Awake()
	{
	
	}

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
