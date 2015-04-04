using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharPanel : MonoBehaviour {
	private const string CHILDPASS = "AnchorBottom/";
	private const string PICTURE = "Picture";
	private const string NAME = "Name";
	private const string GETPANEL = "GetPanel";
	private string ORDERNUM;
	private const string PATHCOLLECTION= "COLLECTIONCharacter/Lv";
	private const string PATHNAME= "chick_name/chick_name_";
	private const string SHOWCHICK = "_1";
	private const string HIDECHICK = "_0";

	private Dictionary <string, UISprite> childSpriteDic = new Dictionary<string, UISprite >();
	UILabel levelLabel;

	void Awake () 
	{
		ORDERNUM = this.gameObject.name.Substring(this.gameObject.name.Length - 2);

		childSpriteDic.Add(PICTURE, ChildUISprite(PICTURE));
		childSpriteDic.Add(NAME, ChildUISprite(NAME));
		childSpriteDic.Add(GETPANEL, ChildUISprite(GETPANEL));

		levelLabel = transform.FindChild(CHILDPASS + "LevelLabel").GetComponent<UILabel>();
		levelLabel.text = "レベル" + (int.Parse(ORDERNUM) + 1).ToString();

		childSpriteDic[NAME].spriteName = PATHNAME + ORDERNUM;
		childSpriteDic[NAME].enabled = false;
	}

	void Start () 
	{

	}

	public void ShowFirstTimeChar()
	{
		PanelAnimation();
	}

	public void ShowChar(bool on)
	{
		string path = PATHCOLLECTION + ORDERNUM;
		if(on){
			childSpriteDic[PICTURE].spriteName = path + SHOWCHICK;
			childSpriteDic[NAME].enabled = true;
			levelLabel.gameObject.SetActive(false);
			childSpriteDic[GETPANEL].enabled = true;
		}
		else{
			childSpriteDic[PICTURE].spriteName = path + HIDECHICK;
		}
	}

	private void PanelAnimation()
	{
		Animation anim = childSpriteDic[GETPANEL].GetComponent<Animation>();
		anim.clip.wrapMode =WrapMode.Loop;
		anim.Play();
	}

	UISprite ChildUISprite(string name)
	{
		return transform.FindChild(CHILDPASS + name).GetComponent<UISprite>();
	}

	void Update()
	{

	}
	

}
