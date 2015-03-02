using UnityEngine;
using System.Collections;

public class MoveBasket : MonoBehaviour {

	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash( "x", -3.0f, "time", 3.5f, "easeType", "linear", "loopType", "pingPong", "delay", 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
