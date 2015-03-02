using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterManager : MonoBehaviour 
{	
	private string gameObjName {
		get{return gameObject.name;}
	}
	Collider[] allCollider;

	public int thisCharScore
	{
		get;
		private set;
	}

	private float timer = 0f;

	void Awake()
	{
		allCollider = GetComponentsInChildren<Collider>();
		string index = gameObjName.Substring(gameObjName.Length - 3);
		thisCharScore =int.Parse(index);
	}

	void Start () 
	{

	}



	public void DestroyCollider()
	{
		foreach (var eachCollider in allCollider) 
		{
			eachCollider.enabled = false;
		}
	}






	
	void Update () 
	{
		timer += Time.deltaTime;

		//ChildHasMeshRender.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
		//transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
	}
}
