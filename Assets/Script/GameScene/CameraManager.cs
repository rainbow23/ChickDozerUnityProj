using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	void Awake()
	{
	
	}
	
	void Start () {
		//ipad
		if(UIRootManager.Aspect >= 75 ){
			Camera.main.fieldOfView = 66f;
			Camera.main.transform.setLocalPositionY(5.5f);
		}
		//iPhone4
		else if(UIRootManager.Aspect >= 67){
			Camera.main.fieldOfView = 73f;
			Camera.main.transform.setLocalPositionY(5.4f);
		}
		//iPhone5
		else if(UIRootManager.Aspect >= 56 ){
			Camera.main.fieldOfView = 76f;
			Camera.main.transform.setLocalPositionY(5.7f);
		}
	}
	
	void Update () {
	
	}
}
