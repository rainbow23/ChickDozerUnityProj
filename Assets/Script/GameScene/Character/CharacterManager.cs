using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterManager : MonoBehaviour 
{	
	private string gameObjName { get{return gameObject.name;} }
	Collider[] allCollider;

	public int thisCharScore
	{
		get;
		private set;
	}

	void Awake()
	{
		allCollider = GetComponentsInChildren<Collider>();
		string index = gameObjName.Substring(gameObjName.Length - 3);
		thisCharScore =int.Parse(index);
	}

	void Start () 
	{

	}



	public void DisableCollider()
	{
		foreach (var eachCollider in allCollider) 
		{
			eachCollider.enabled = false;
		}
	}






	
	void Update () 
	{

		//ChildHasMeshRender.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
		//transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
	}
}
