using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharPanel : MonoBehaviour {
	private const string ChildPass = "AnchorBottom/";
	private const string Picture = "Picture";
	private const string Name = "Name";
	private const string GetPanel = "GetPanel";
	private string OrderNum;
	private const string PathCollection= "COLLECTIONCharacter/Lv";
	private const string PathName= "chick_name/chick_name_";
	private const string ShowChick = "_1";
	private const string HideChick = "_0";

	private Dictionary <string, UISprite> childSpriteDic = new Dictionary<string, UISprite >();
	UILabel levelLabel;

	void Awake () 
	{
		OrderNum = this.gameObject.name.Substring(this.gameObject.name.Length - 2);

		childSpriteDic.Add(Picture, ChildUISprite(Picture));
		childSpriteDic.Add(Name, ChildUISprite(Name));
		childSpriteDic.Add(GetPanel, ChildUISprite(GetPanel));

		levelLabel = transform.FindChild(ChildPass + "LevelLabel").GetComponent<UILabel>();
		levelLabel.text = "レベル" + (int.Parse(OrderNum) + 1).ToString();

		childSpriteDic[Name].spriteName = PathName + OrderNum;
		childSpriteDic[Name].enabled = false;
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
		string path = PathCollection + OrderNum;
		if(on){
			childSpriteDic[Picture].spriteName = path + ShowChick;
			childSpriteDic[Name].enabled = true;
			levelLabel.gameObject.SetActive(false);
			childSpriteDic[GetPanel].enabled = true;
		}
		else{
			childSpriteDic[Picture].spriteName = path + HideChick;
		}
	}

	private void PanelAnimation()
	{
		Animation anim = childSpriteDic[GetPanel].GetComponent<Animation>();
		anim.clip.wrapMode =WrapMode.Loop;
		anim.Play();
	}

	UISprite ChildUISprite(string name)
	{
		return transform.FindChild(ChildPass + name).GetComponent<UISprite>();
	}

	void Update()
	{

	}
	

}
