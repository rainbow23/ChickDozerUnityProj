using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Collection : MonoBehaviour {
	#region variable
	public static int allChickCount =33;
	public bool showAllChickCollection = false;
	
	GameObject[] eachObjNameisNew = new GameObject[33];
	
	Transform[] ItemsTransform = new Transform[33];
	//Transform UIGridTransform;
	
	UISprite[] chickUISprites = new UISprite[33];
	UISprite[] collectionBgUISprites = new UISprite[33];
	List<UISprite> collectionList = new List<UISprite>();
	
	UISprite[] chickNames = new UISprite[33];
	
	string silhouette = "_0";		
	string displayChick = "_1";
	#endregion
	
	void Awake(){
		//UIGridTransform = GameObject.Find("UIGrid").GetComponent<Transform>();
		initializeChickUISprites();
		setEachChickLabels();
	}
	
	void initializeChickUISprites(){
		for(int i =0; i < 33; i++){
			string ItemNum = (i + 1).ToString(); 
			chickUISprites[i] = GameObject.Find(ItemNum + "Item").transform.GetChild(0).GetChild(0).GetComponent<UISprite>();
		}
	}
	
	void setEachChickLabels(){
		for(int i =0; i < 33; i++){
			string ItemNum = (i + 1).ToString(); 
			chickNames[i] = GameObject.Find(ItemNum + "Item").transform.GetChild(0).FindChild("Name").GetComponent<UISprite>();
			
			if(i < 10){
				chickNames[i].spriteName = "chick_name/chick_name_0" + i.ToString();	
			}
			else{
				chickNames[i].spriteName = "chick_name/chick_name_" + i.ToString();	
			}
			
		}
	}
	
	
	void Start () {
		showHideAllChickCollection();
		hideAllGetPlate();
		showFirstGetChikPlate();
		showOnlygetChick();
	}
	
	void showHideAllChickCollection(){
		
		string hideOrShow;
		
		if(!showAllChickCollection){
			hideOrShow = silhouette;
		}
		else{
			hideOrShow = displayChick;
		}
		
		for(int i =0; i < 33; i++){
			string ItemNum = (i + 1).ToString(); 			
			//最初に全てひよこ非表示
			if(i < 9){
				chickUISprites[i].spriteName = "COLLECTIONCharacter/Lv0" + ItemNum + hideOrShow;
				chickUISprites[i].MakePixelPerfect();
				chickUISprites[i].depth = 1;
			}
			else{
				chickUISprites[i].spriteName = "COLLECTIONCharacter/Lv" + ItemNum + hideOrShow;
				chickUISprites[i].MakePixelPerfect();
				chickUISprites[i].depth = 1;
			}
		}
	}
	
	//Hide Newパネル 
	void hideAllGetPlate(){
		for(int i = 0; i < allChickCount; i++){
			string num = (i + 1).ToString();
			eachObjNameisNew[ i ] = GameObject.Find(num + "Item").transform.GetChild(0).FindChild("New").gameObject;
			eachObjNameisNew[ i ].SetActive(false);
			//print ("eachObjNameisNew: " + eachObjNameisNew[i -1].ToString());
		}
	}
	
	
	void showFirstGetChikPlate(){
		int chickNum = 0;
		string key;
		while(chickNum < allChickCount){
			key = "chickHitFirst" + chickNum.ToString();
			//print ("Load: " + PlayerPrefs.GetInt(key));
			
			if(PlayerPrefs.GetInt(key) == 1){
				eachObjNameisNew[chickNum].SetActive(true);
				
				eachObjNameisNew[chickNum].animation["newChickSignBoard"].wrapMode = WrapMode.Loop;
				eachObjNameisNew[chickNum].animation.Play("newChickSignBoard");
				StartCoroutine(hideSignBoardWhenAnimIsFinished(chickNum));
				
				
				//表示したらフラグを変える
				PlayerPrefs.SetInt(key, -1);
			}
			chickNum += 1;
		}
	}
	
	
	IEnumerator hideSignBoardWhenAnimIsFinished(int receiveChickNum){
		while(eachObjNameisNew[receiveChickNum].animation.isPlaying){
			yield return null;
		}
		eachObjNameisNew[receiveChickNum].SetActive(false);
	}
	
	void  showOnlygetChick(){
		int chickNum = 0;
		string key;
		string ItemNum;
		
		while(chickNum < allChickCount){
			key = "chickHitFirst" + chickNum.ToString();	
			ItemNum = (chickNum + 1).ToString();
			
			//過去に獲得したひよこ表示
			if(PlayerPrefs.GetInt(key) != 0){
				
				if(chickNum < 10){
					chickUISprites[chickNum].spriteName = "COLLECTIONCharacter/Lv0" + ItemNum + displayChick;
					chickUISprites[chickNum].depth = 1;
				}
				else{
					chickUISprites[chickNum].spriteName = "COLLECTIONCharacter/Lv" + ItemNum + displayChick;
					chickUISprites[chickNum].depth = 1;
				}
			}
			//Display New Panel
			else if(PlayerPrefs.GetInt(key) == 1){
				eachObjNameisNew[chickNum].SetActive(true);
				eachObjNameisNew[chickNum].animation["newChickSignBoard"].wrapMode = WrapMode.Loop;
				eachObjNameisNew[chickNum].animation.Play("newChickSignBoard");
				//表示したらフラグを変える
				PlayerPrefs.SetInt(key, -1);
			}
			
			chickNum += 1;
		}
		
		/*
		for(int i =0; i < 33; i++){
			string ItemNum = (i + 1).ToString();
			print ("chick: " + PlayerPrefs.GetInt("chick" + i) );
			if(PlayerPrefs.GetInt("chick" + i ) == 1){
				if(i < 9){
					chickUISprites[i].spriteName = "COLLECTIONCharacter/Lv0" + ItemNum + displayChick;
					chickUISprites[i].depth = 1;
				}
				else{
					chickUISprites[i].spriteName = "COLLECTIONCharacter/Lv" + ItemNum + displayChick;
					chickUISprites[i].depth = 1;
				}
			}
		}
		*/
	}
	
	
	
	
	
	
	public void gotoGameScene(){
		FadeManager.Instance.LoadLevel("StageCastle2", 0.5f);
	}
	
	
	void Update () {
	
	}
}
