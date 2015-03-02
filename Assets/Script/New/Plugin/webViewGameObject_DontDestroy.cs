using UnityEngine;
using System.Collections;

public class webViewGameObject_DontDestroy : MonoBehaviour {
	
	public static bool dontDestroyWebViewGameObject = true;
	// Use this for initialization
	void Awake () {
		if(dontDestroyWebViewGameObject){
			DontDestroyOnLoad(this.gameObject);
			dontDestroyWebViewGameObject =false;
		}
		else{
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
