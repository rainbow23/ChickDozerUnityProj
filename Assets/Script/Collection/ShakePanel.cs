using UnityEngine;
using System.Collections;

public class ShakePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		shake();
	}
	
	void shake(){
		iTween.ShakeRotation(gameObject, iTween.Hash("z", 30, "looptype", "loop"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
