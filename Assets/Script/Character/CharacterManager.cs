using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour 
{	
	Animation meshAnim;
	GameObject ChildHasMeshRender;
	void Start () 
	{
		ChildHasMeshRender = GetComponentInChildren<MeshRenderer>().gameObject;
	}
	
	void Update () 
	{
		//ChildHasMeshRender.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
		//transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
	}
}
