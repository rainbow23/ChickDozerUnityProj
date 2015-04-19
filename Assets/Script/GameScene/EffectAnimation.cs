using UnityEngine;
using System.Collections;

public class EffectAnimation : MonoBehaviour {

	Transform thisTransform;

	void Awake()
	{
		thisTransform = GetComponent<Transform>();
	}

	void Start () {
	
	}

	public void MoveUp(){
		float reachPos = thisTransform.localPosition.y + 50f;

		iTween.MoveTo(gameObject, 
					iTween.Hash( 
		            "islocal", true,
		            "y", reachPos, "time", 0.5f, 
		            "easeType", "linear", 
		            "loopType", "once", 
		            "oncomplete", "Destroy", "oncompletetarget", gameObject,
		            "delay", 0 
		            ));
		//Debug.Break();
	}

	void Destroy(){
		Destroy(this.gameObject);
	}

	void Update () {
	
	}
}
