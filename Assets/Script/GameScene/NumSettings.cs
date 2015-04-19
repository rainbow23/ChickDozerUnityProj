using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NumSettings : MonoBehaviour {

	private UISprite sprite;
	private UISprite childSprite;
	private bool hasChild = false;
	public bool activeSelf{private set; get;}
	private List<Transform> thisTransformGrp = new List<Transform>();


	void Awake(){
		sprite = GetComponent<UISprite>();

		if(transform.childCount != 0){
			hasChild = true;
			childSprite = transform.GetChild(0).GetComponent<UISprite>();
		}

		thisTransformGrp.Add(this.transform);
		if(hasChild)
		{
			thisTransformGrp.Add(transform.GetChild(0).GetComponent<Transform>());
		}

		activeSelf = sprite.enabled;
	}

	void Start () {
	
	}

	public void MoveToX(float reachPos)
	{
		thisTransformGrp[0].setLocalPositionX(reachPos);
		if(hasChild){
			thisTransformGrp[1].setLocalPositionX(0f);
		}
	}

	public void AddMoveToX(float addPos)
	{
		foreach (var each in thisTransformGrp) {
			each.transform.addLocalPositionX(addPos);
		}
	}

	public void First1NumOfChildMoveToX(float reachPos){
		if(hasChild){
			childSprite.transform.setLocalPositionX(reachPos);
			//Debug.Log("move child", gameObject);
		}
	}

	public void Show(bool onOff)
	{
		if(hasChild){
			sprite.enabled = onOff;
			childSprite.enabled = onOff;
		}
		else{
			sprite.enabled = onOff;
		}
		activeSelf = onOff;
	}

	public void SetSpriteName(string path){
		sprite.spriteName = path;
	}



	void Update () {
	
	}
}
