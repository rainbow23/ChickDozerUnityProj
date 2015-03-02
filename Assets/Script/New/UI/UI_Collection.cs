using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Collection : MonoBehaviour {
	#region variable
	PlugInObject PlugInObjectScript;

	public static int allChickCount =33;
	public bool showAllChickCollection = false;
	
	GameObject[] eachObjNameisNew = new GameObject[33];
	
	Transform[] ItemsTransform = new Transform[33];
	//Transform UIGridTransform;
	Transform BackButtonTransform;
	
	UISprite[] chickUISprites = new UISprite[33];
	UISprite[] collectionBgUISprites = new UISprite[33];
	List<UISprite> collectionList = new List<UISprite>();
	
	UISprite[] chickNames = new UISprite[33];
	
	string silhouette = "_0";		
	string displayChick = "_1";
	#endregion
	
	UIAtlas atlas;
	
	int showPopUpNum;
	
	void Awake(){


		
		if(GameObject.Find("webViewGameObject")  == true){
			PlugInObjectScript = GameObject.Find("webViewGameObject").GetComponent<PlugInObject>();
		}
		BackButtonTransform = GameObject.Find("BackButton").GetComponent<Transform>(); 
		atlas = Resources.Load("Texture/newAtlas/2DUI/TitleCollectionAtlas", typeof(UIAtlas)) as UIAtlas;
		UISprite[] allUISprites = FindObjectsOfType(typeof(UISprite)) as UISprite[];
		
		foreach(var eachUISprites in allUISprites){
			eachUISprites.atlas = atlas;
		}
		
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
		scrollFitIPad();
	}
	
	void scrollFitIPad(){
		//UIRoot
		UIRoot ViewUIRoot = GameObject.Find("View UI").GetComponent<UIRoot>();
		
		
		// 1 of vertical rows change position
		int rowsOf1 = 1;
		while(rowsOf1 < 34){
			string rowsOf1String = rowsOf1.ToString();
			Transform rowsOf1Transform =  GameObject.Find(rowsOf1String + "Item").GetComponent<Transform>(); 
			
			if(UIRootManager.Aspect >= (float)ScreenAspect.iPad){//iPad
				rowsOf1Transform.setLocalPositionX(-333f);	
			}
			else{
				rowsOf1Transform.setLocalPositionX(-305f);	
			}
			rowsOf1 += 3;
		}
		
		
		
		if(UIRootManager.Aspect >= (float)ScreenAspect.iPad){//iPad
			ViewUIRoot.manualHeight = 1024;
			
			
			// 2 and 3 of vertical rows change position
			int rowsOf2 = 2;
			while(rowsOf2 < 34){
				string rowsOf2String = rowsOf2.ToString();
				Transform rowsOf2Transform =  GameObject.Find(rowsOf2String + "Item").GetComponent<Transform>(); 
				rowsOf2Transform.setLocalPositionX(-98f);
				rowsOf2 += 3;
			}
			
			int rowsOf3 = 3;
			while(rowsOf3 < 34){
				string rowsOf3String = rowsOf3.ToString();
				Transform rowsOf3Transform =  GameObject.Find(rowsOf3String + "Item").GetComponent<Transform>(); 
				rowsOf3Transform.setLocalPositionX(142f);
				rowsOf3 += 3;
			}
			
			BackButtonTransform.setLocalPositionX(64f);
		}
		
		else if(UIRootManager.Aspect >= (float)ScreenAspect.inch4){//iphone4
			ViewUIRoot.manualHeight = 960;
		}
		else{//iphone5
			ViewUIRoot.manualHeight = 1136;
		}
		
		
		//iphone4,5
		if(UIRootManager.Aspect < (float)ScreenAspect.iPad){
			BackButtonTransform.setLocalPositionX(24.5f);	
			
			// 2 and 3 of vertical rows change position
			int rowsOf2 = 2;
			while(rowsOf2 < 34){
				string rowsOf2String = rowsOf2.ToString();
				Transform rowsOf2Transform =  GameObject.Find(rowsOf2String + "Item").GetComponent<Transform>(); 
				rowsOf2Transform.setLocalPositionX(-98f);
				rowsOf2 += 3;
			}
			
			int rowsOf3 = 3;
			while(rowsOf3 < 34){
				string rowsOf3String = rowsOf3.ToString();
				Transform rowsOf3Transform =  GameObject.Find(rowsOf3String + "Item").GetComponent<Transform>(); 
				rowsOf3Transform.setLocalPositionX(107f);
				rowsOf3 += 3;
			}
		}	
		
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
				
				//Display New Panel
				eachObjNameisNew[chickNum].SetActive(true);
				eachObjNameisNew[chickNum].animation["newChickSignBoard"].wrapMode = WrapMode.Loop;
				eachObjNameisNew[chickNum].animation.Play("newChickSignBoard");
				//表示したらフラグを変える
				PlayerPrefs.SetInt(key, -1);
				
				
				//6つ取得するごとにレビューポップアップを表示
				bool show = false;
				
				switch(chickNum){	
					case(5):
					case(11):
					case(17):
					case(23):
					case(29):
					case(32):
						show = true;
						break;
				}
				
				//print ("show: " + show);
				
				showPopUpNum = PlayerPrefs.GetInt("showPopUpNum",6);
				
				if(show){
					if(showPopUpNum != 0){
						PlugInObjectScript.showReviewPopUp();
						//print ("Show Review PopUP: " + showPopUpNum);
						
						showPopUpNum -=1;
						PlayerPrefs.SetInt("showPopUpNum", showPopUpNum);
					}
				}
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
		bool showReviewPopUpFlag = false;
				
		while(chickNum < allChickCount){
			key = "chickHitFirst" + chickNum.ToString();	
			ItemNum = (chickNum + 1).ToString();
			
			if(PlayerPrefs.GetInt(key) != 0){
				//過去に獲得したひよこ表示
				if(chickNum < 9){
					chickUISprites[chickNum].spriteName = "COLLECTIONCharacter/Lv0" + ItemNum + displayChick;
					chickUISprites[chickNum].depth = 1;
				}
				else{
					chickUISprites[chickNum].spriteName = "COLLECTIONCharacter/Lv" + ItemNum + displayChick;
					chickUISprites[chickNum].depth = 1;
				}	
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
