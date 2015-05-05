using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class CharacterManager : MonoBehaviour 
{	
	public IFlyBehavior flyBehavior;

	public void SetPerformFly(IFlyBehavior iFlyBehavior)
	{
		flyBehavior = iFlyBehavior;
	}

	public  void PeformFly()
	{
		flyBehavior.Fly();
	}

	public string gameObjName { get{return gameObject.name;} }
	private int _thisCharNum;
	public int thisCharNum
	{
		get{ return _thisCharNum;}
		set{ _thisCharNum = value;}
	}

	void OnEnable()
	{

	}

	protected virtual void Awake()
	{

	}

	protected virtual void Start () 
	{
		string index = gameObjName.Substring(gameObjName.Length - 2);
		thisCharNum =int.Parse(index);
	}

	void Update () 
	{
	
	}
}
