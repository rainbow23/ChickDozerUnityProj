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

	void OnEnable()
	{

	}

	protected virtual void Awake()
	{

	}

	protected virtual void Start () 
	{
		string index = gameObjName.Substring(gameObjName.Length - 3);
		thisCharScore =int.Parse(index);
	}

	void Update () 
	{
	
	}
}
