using UnityEngine;
using System.Collections;

public class HudBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void destroythisGameObject(){
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.y >= 80f){//62.5f){
	//		Destroy(gameObject);
			
		}
	}
}
