using UnityEngine;
using System.Collections;

public class ButtonMessageCustomize : UIButtonMessage {

	void Start () {
	
	}
	
	void Update () {
		if(target == null){
			if(functionName == "volumeOnOff"){
				target = GameObject.Find("SoundManager");
			}
		}
		else{ return;}
	}	

}
