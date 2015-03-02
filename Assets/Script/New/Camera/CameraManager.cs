using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	float aspect;
	
	void Awake(){
		aspect = (float)Screen.width / (float)Screen.height;
		aspect = Mathf.Round(aspect * 100);
	}
	
	void Start () {
		//iPhone4
		if(aspect >= 67){
			Camera.main.fieldOfView = 72f;
		}
		//iPhone5
		else if(aspect >= 56 ){
			Camera.main.fieldOfView = 71f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
