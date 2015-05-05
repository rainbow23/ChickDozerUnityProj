using UnityEngine;
using System.Collections;

public class EffectAnimation : MonoBehaviour {


	GameObject particle;
	Transform thisTransform;
	Transform bottomTransform;

	ParticleSystem  particleSystems;

	void Awake()
	{
		bottomTransform = GameObject.Find ("Bottom").GetComponent<Transform>();
		thisTransform = GetComponent<Transform>();

		//effectObj = Resources.LoadAssetAtPath("Assets/JMO Assets/Cartoon FX/CFX2 Prefabs (Mobile)/Pickup Items/CFXM2_PickupHeart3.prefab", typeof(GameObject)) as GameObject;


		particleSystems = GetComponent<ParticleSystem>();
	}

	void Start () {
	
	}

	public void MoveUp(){
//		particleSystems.Play();
		/*
		GameObject obj = Instantiate(effectObj, Vector3.zero, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
		obj.transform.parent = thisTransform.parent;
		obj.transform.localPosition = thisTransform.localPosition;// new Vector3(0f, 0f, 0f);
		obj.transform.localScale = (Vector3.one);
		*/
		float reachPos = thisTransform.localPosition.y + 50f;

		//Debug.Break();
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



	void Destroy()
	{
		this.gameObject.SetActive(false);

		//Destroy(this.gameObject);
	}

	void Update () {
	
	}
}
