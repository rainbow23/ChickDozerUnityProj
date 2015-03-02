using UnityEngine;
using System.Collections;

public class MonoBehaviourExtends : MonoBehaviour {

	public static GameObject InstantiateGameObjects(GameObject obj, Vector3 pos){
		GameObject go = Instantiate(obj) as GameObject;
		go.transform.localPosition = pos;
		if(go == null)
			Debug.LogError("obj is nothing");
		return go;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
