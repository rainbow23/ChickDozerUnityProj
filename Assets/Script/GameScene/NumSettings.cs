using UnityEngine;
using System.Collections;

public class NumSettings : MonoBehaviour {

	private UISprite sprite;
	private UISprite childSprite;
	private bool hasChild = false;
	public bool activeSelf{private set; get;}


	void Awake(){
		sprite = GetComponent<UISprite>();

		if(transform.childCount != 0){
			hasChild = true;
			childSprite = transform.GetChild(0).GetComponent<UISprite>();
		}
		activeSelf = sprite.enabled;
	}

	void Start () {
	
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
