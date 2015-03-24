using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharPanel : MonoBehaviour {

	private const string CHILDPASS = "AnchorBottom/";
	private const string PICTURE = "Picture";
	private const string NAME = "Name";
	private const string GETPANEL = "GetPanel";

	private const string PATHCOLLECTION= "COLLECTIONCharacter/Lv";
	private const string SHOWCHICK = "_1";
	private const string HIDECHICK = "_0";
	//01_0

	private Dictionary <string, UISprite> childSpriteDic = new Dictionary<string, UISprite >();
	UILabel levelLabel;

	void Awake () 
	{
		childSpriteDic.Add(PICTURE, ChildUISprite(PICTURE));
		childSpriteDic.Add(NAME, ChildUISprite(NAME));
		childSpriteDic.Add(GETPANEL, ChildUISprite(GETPANEL));
		levelLabel = GameObject.Find(CHILDPASS + "LevelLabel").GetComponent<UILabel>();

	}

	void Start () {
	
	}

	public void ShowChar(bool on)
	{
		if(on)
			childSpriteDic[PICTURE].spriteName = PATHCOLLECTION + "01" + SHOWCHICK;
		else
			childSpriteDic[PICTURE].spriteName = PATHCOLLECTION + "01" + HIDECHICK;
	}

	public void ShowPanel()
	{
		Animation anim = childSpriteDic[GETPANEL].GetComponent<Animation>();
		anim.Play();
	}

	UISprite ChildUISprite(string name)
	{
		return transform.FindChild(CHILDPASS + name).GetComponent<UISprite>();
	}
	

}
