using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {
	
	public static bool original = false;
	
	void Awake () {
		if(original == false){
			DontDestroyOnLoad(this.gameObject);
			print("original: " + original);
			original = true;
		}
		else{
			print("original: " + original);
			Destroy(this.gameObject);
		}
	}
	
	void Start () {
	
	}
	
	void Update () {
	
	}
}
