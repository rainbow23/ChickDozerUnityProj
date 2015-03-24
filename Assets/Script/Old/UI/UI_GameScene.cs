using UnityEngine;
using System.Collections;

public class UI_GameScene : MonoBehaviour {
	#region variable
	public delegate void SceneHandler();
	public static event SceneHandler _startTransition;
	public static event SceneHandler _feverTimeEvent; //horynAppPurchaseuu
	//public static event SceneHandler _isMoveChick; //horynAppPurchaseuu
	
	FadeManager FadeManagerScript;
	PlugInObject PlugInObjectScript;
	ScoreManager ScoreManagerScript;
	public Transform pointGrpTransform;
	public Transform levelGrpTransform;
	Transform LevelUpSpriteTransform;
	GameObject InAppPurchaseMenu;
	GameObject Timer;
	GameObject newChickSignBoard;
	
	UIRoot uiRoot;
	
	UISprite ButtonToTitleUISprite;
	UISprite BackgroundUISprite;
	
	public UISprite howToUISprite;
	public UISprite[] pointDigits = new UISprite[5];
	public UISprite[] levelDigits = new UISprite[2];
	public UISprite[] StockChicksUISprite = new UISprite[4];
	UISprite BgCastle; 
	UISprite Tree1;
	UISprite Tree2;
	UISprite[] digitUISprite = new UISprite[4];
	UISprite percentateUISprite;
	UISprite feverTimeGageUISprite;
	UISprite colonUISprite;
	UILabel FeverTimerLabel;
	
	float aspect;
	#endregion
	
	void Awake(){
		ButtonToTitleUISprite = GameObject.Find("ButtonToTitle").GetComponent<UISprite>();
		BackgroundUISprite = GameObject.Find("Background").GetComponent<UISprite>();
		howToUISprite = GameObject.Find("HowToUIScene").GetComponent<UISprite>();
		Tree1 = GameObject.Find("Tree1").GetComponent<UISprite>();
		Tree2 = GameObject.Find("Tree2").GetComponent<UISprite>();
		uiRoot = GameObject.Find("UI Root").GetComponent<UIRoot>();
		BgCastle = GameObject.Find("BgCastle").GetComponent<UISprite>();
		
		InAppPurchaseMenu = GameObject.Find("InAppPurchaseMenu");
		pointGrpTransform = GameObject.Find("PointGrp").GetComponent<Transform>();
		levelGrpTransform = GameObject.Find("LvGrp").GetComponent<Transform>();
		percentateUISprite = GameObject.Find("Percentage").GetComponent<UISprite>();
		feverTimeGageUISprite = GameObject.Find("FeverGage").GetComponent<UISprite>();
		colonUISprite = GameObject.Find("Colon").GetComponent<UISprite>();
		FeverTimerLabel = GameObject.Find("FeverTimerLabel").GetComponent<UILabel>();
		
		FadeManagerScript = GameObject.Find("FadeManager").GetComponent<FadeManager>(); 
		PlugInObjectScript = GameObject.Find("webViewGameObject").GetComponent<PlugInObject>();
		ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		Timer = GameObject.Find("Timer");
		newChickSignBoard = GameObject.Find("newChickSignBoard");
		
		aspect = (float)Screen.width / (float)Screen.height;
		aspect = Mathf.Round(aspect * 100);
		
		//start配置だと実機で落ちる
		//setHowTo();
		
	}
	
	void Start(){
		PlugInObjectScript.reportGameStartToFlurry();
		//TargetがBottomに強制変更される
		//BgCastle.topAnchor.target = GameObject.Find("BgCamera").transform;
		
		//ipad
		if(aspect >= 75 ){
			BgCastle.SetDimensions(870, 1575);
			BgCastle.transform.setLocalPositionY(-100f);
			Tree1.transform.setLocalPositionY(-60f);
			Tree2.transform.setLocalPositionY(-140f);
		}
		//iPhone4
		else if(aspect >= 67){
			BgCastle.SetDimensions(663, 1200);
			BgCastle.transform.setLocalPositionY(-90f);
			Tree1.transform.setLocalPositionY(18f);
			Tree2.transform.setLocalPositionY(-22f);
		}
		//iPhone5
		else if(aspect >= 56 ){
			BgCastle.SetDimensions(663, 1200);
			BgCastle.transform.setLocalPositionY(-130f);
			Tree1.transform.setLocalPositionY(-21f);
			Tree2.transform.setLocalPositionY(-90f);
		}
		
		//print ("Tree2 posY: " + Tree2.transform.localPosition.y);
		
		
//		PlugInObjectScript.asutarisukuLoad();
		bitmapScoreInitialize();
		bitmapLvInitialize();
		bitmapScoreUpdate(ScoreManager.point);
		bitmapLvUpdate(ScoreManager.levelNum);
		bitmapPercentageUpdate(ScoreManager.nextLevelPercentage);
		stockChicksUISpriteInitialize();
		feverTimerInitialize();
		//bitmapFeverTimerInitialize();
		newChickSignBoard.SetActive(false);
		
		LevelUpSpriteTransform = GameObject.Find("LevelUpSprite").GetComponent<Transform>();
		
		//_isMoveChick(); // Delegate in scene All chicks and Fever Time is Start UI_FeverTime.cs
	}
	
	#region Initialize
	//When Scene is Start
	void loadDataFromScoreManagerScript(){	
		bitmapScoreInitialize();
		bitmapLvInitialize();
		bitmapScoreUpdate(ScoreManager.point);
		bitmapLvUpdate(ScoreManager.levelNum);
		bitmapPercentageUpdate(ScoreManager.nextLevelPercentage);
		bitmapFeverTimerInitialize();
		UIFeverTime(ScoreManager.countSecond , ScoreManager.countMinusSecond);
		if(ScoreManager.feverTimeFlag){
			hideGageAndTimer();
		}
	}
	
	/*
	public void setHowTo(){
		if(PlayerPrefs.GetInt("loadSceneFromCollection") == 0 ){
			howToUISprite.transform.setLocalPositionX(0f);
			//print ("aspect: " + aspect);
			
			//ipad
			if(aspect == 75){
				howToUISprite.transform.setLocalPositionX(0f);
				howToUISprite.transform.setLocalPositionY(-76f);
				howToUISprite.SetDimensions(503, 640);
			}
			//iPhone4
			else if(aspect >= 67){
				howToUISprite.transform.setLocalPositionX(0f);
				howToUISprite.transform.setLocalPositionY(0f);
				howToUISprite.SetDimensions(443, 563);
			}
			//iPhone5
			else if(aspect >= 56 ){
				howToUISprite.transform.setLocalPositionX(0f);
				howToUISprite.transform.setLocalPositionY(-95f);
			}
		}
	}
	*/
	
	/*
	void hideHowTo(){
		if(FadeManagerScript.isFading ){return;}
		if(FadeManagerScript.isFading == false){
			StartCoroutine(moveChick());
		}
		
	}
	
	bool once = false;
	IEnumerator moveChick(){
		yield return new WaitForSeconds(0.15f);
		if(once == false){
			//print ("check");
			_isMoveChick(); // Delegate in scene All chicks and Fever Time is Start UI_FeverTime.cs
			howToUISprite.transform.setLocalPositionX(4000f);
			once = true;
		
			if(ScoreManager.feverTimeFlag){
				_feverTimeEvent(); // delegate UI_GameScene.cs ChickManager.cs, UI_FeverTime.cs SoundManager.cs
			}
		}
	}
	*/
	
	void stockChicksUISpriteInitialize(){
		for(int i = 0; i < 4; i++){
			string num = (i + 1).ToString();
			StockChicksUISprite[i] = GameObject.Find("StockChick" + num).GetComponent<UISprite>();
		}
	}
	
	void bitmapScoreInitialize(){
		if(pointDigits[0] == null){	
			for(int i =0; i < 5; i++){
				pointDigits[i] = pointGrpTransform.FindChild("Point" + (i + 1).ToString() + "Digit").GetComponent<UISprite>();
			}
		}
	}
	
	void bitmapLvInitialize(){
		for(int i = 0; i < 2; i++){
			levelDigits[i] = GameObject.Find("LvScore" + (i + 1).ToString()).GetComponent<UISprite>();
		}
	}
	
	void bitmapFeverTimerInitialize(){
		if(ScoreManager.feverTimeFlag){return;}
			for(int i = 0; i < 4; i++){
				if(GameObject.Find("Digit" + (i + 1).ToString()) == null ){continue;}
				digitUISprite[i] = GameObject.Find("Digit" + (i + 1).ToString()).GetComponent<UISprite>();
				digitUISprite[i].enabled = true;
			}
	}
	
	void feverTimerInitialize(){
		for(int i = 0; i < 4; i++){
				if(digitUISprite[i] == null){
					digitUISprite[i] = GameObject.Find("Digit" + (i + 1).ToString()).GetComponent<UISprite>();
					digitUISprite[i].enabled = true;
				}
			}
	}
	#endregion
	
	void showPlate(){
		newChickSignBoard.SetActive(true);
		newChickSignBoard.animation["newChickSignBoard"].wrapMode = WrapMode.Once;
		newChickSignBoard.animation.Play("newChickSignBoard");
		StartCoroutine(hideSignBoardWhenAnimIsFinished());
	}
	
	IEnumerator hideSignBoardWhenAnimIsFinished(){
		while(newChickSignBoard.animation.isPlaying){
			yield return null;
		}
		newChickSignBoard.SetActive(false);
	}
	
	/// <summary>
	/// ひよこを作らないFlag発動
	/// </summary>
	public void showInAppPurchaseMenu(){
		iTween.MoveTo(InAppPurchaseMenu,
			iTween.Hash("x", 0f,
								"islocal", true,
								"time", 0.7f,
								"easeType", "easeInCubic"
			));
		ScoreManagerScript.touchNguiButton();
	}
	
	/// <summary>
	/// animation終了後ひよこを作るFlag発動
	/// </summary>
	public void hideInAppPurchaseMenu(){
		iTween.MoveTo(InAppPurchaseMenu,
			iTween.Hash("x", -1000f,
								"islocal", true,
								"time", 0.7f,
								"oncompletetarget", ScoreManagerScript.gameObject,
								"onComplete", "arrowCreateChick",
								"easeType", "easeInCubic"
			));
	}
	
	void bitmapScoreUpdate(int receivePoint) {
		//print("Time.timeScale: " + Time.timeScale);
		//print ("receivePoint: " + receivePoint);
		int storeDivideNum = receivePoint;
		int offsetCount = 0;
		
		//ポイント０になったら
		if(storeDivideNum == 0){
			for(int i =0; i < 5; i++){
				if(i == 0){
					pointDigits[i].enabled = true;
					pointDigits[i].spriteName = "" + "0";
				}
				else{
					pointDigits[i].enabled = false;
				}
			}
			return;
		}
		
		//各桁表示
		for(int i =0; i < 5; i++){	
			//initialize
			float offset = i *38f;
			pointDigits[i].transform.setLocalPositionX(170f - offset);
			pointDigits[i].enabled = true;
			
			//01000なら1000とする
			if(storeDivideNum == 0){
				pointDigits[i].enabled = false;
			}
			
			//前の数字が１なら右に詰める
			pointDigits[i].transform.addLocalPositionX(offsetCount * 15f);
			//display number
			int digit = storeDivideNum %10;	
			pointDigits[i].spriteName = "" + digit.ToString();
			pointDigits[i].MakePixelPerfect();
			//prepare for next num
			storeDivideNum /= 10;
			if(digit ==1){ offsetCount += 1;}
		}
	}
	
	void bitmapLvUpdate(int receiveLv){
		int digit1 = receiveLv / 10;
		//if receiveLv is less than 10
		if(digit1 == 0){
			levelDigits[1].enabled = false;
		}
		else{
			levelDigits[1].enabled = true;
		}
		
		int digit0 = receiveLv % 10;
		
		//set Transform
		if(digit0 == 1){
	//		levelDigits[0].transform.setLocalPositionX(55f);
		}
		else{
	//		levelDigits[0].transform.setLocalPositionX(70f);
		}
		
		string digit0String = digit0.ToString();
		string digit1String = digit1.ToString();
		levelDigits[0].spriteName = "" + digit0String;
		levelDigits[1].spriteName = "" + digit1String;
	}
	
	void levelUpEvent(int receiveLv){
		LevelUpSpriteTransform.animation["LevelUp_"].wrapMode = WrapMode.Once;
		LevelUpSpriteTransform.animation.Play("LevelUp_");
		LevelUpSpriteTransform.animation["LevelUp_"].speed = 0.8f;
	}
	
	void gotoCollection(){
		FadeManager.Instance.LoadLevel("Collection", 0.5f);
		_startTransition(); //Delegate :   ScoreManager.cs SaveData
		PlayerPrefs.SetInt("ApplicationPauseCount", 1);
		
	}
	
	bool onceTouch = false;
	void gotoGameScene(){
		if(Time.timeSinceLevelLoad < 1.0f ){return;}
		if(!onceTouch){
			FadeManager.Instance.LoadLevel("Title", 0.5f);
			_startTransition(); //Delegate :   ScoreManager.cs SaveData
			PlayerPrefs.SetInt("ApplicationPauseCount", 1);
			_startTransition(); //Delegate :   ScoreManager.cs SaveData
			PlayerPrefs.SetInt("ApplicationPauseCount", 1);
			onceTouch = true;
		}
		
	}
	
	void bitmapPercentageUpdate(int receiveNextLevelPercentage){
		//print ("receiveNextLevelPercentage:" + receiveNextLevelPercentage);	
		percentateUISprite.spriteName = /*"levelpercent_sprite/" +*/ receiveNextLevelPercentage + "pcnt";
	}
	
	/// <summary>
	/// contact iOS App
	/// </summary>
	public void inAppPurchase(){
		//PlugInObjectScript.ShowAddRanking();
	//	PlugInObjectScript.inAppPurchase();
	}
	
	#region feverTimer
	//カウントだけScoreManagerから貰う
	
	void UIFeverTime(int receiveCountSecond, int receiveCountMinusSecond){
		//percentage
		int maxMinute = 60 * ScoreManager.limitMinute;
		float percentage = ((float)receiveCountSecond / (float)maxMinute) * 100;
		
		//tick
		int second = receiveCountMinusSecond % 60;
		int minutes = receiveCountMinusSecond / 60;
		
		//Fever Time Event
		if(minutes <= 0  &&second <= 0){
			//2回通るのを避けるためflagで判断
			if(ScoreManager.feverTimeFlag == false){
				ScoreManager.feverTimeFlag = true;
				hideGageAndTimer();
				_feverTimeEvent(); // delegate UI_GameScene.cs ChickManager.cs, UI_FeverTime.cs
			}
		}
		else{		
			int digit1Second = second % 10;
			int digit2Second = second / 10;
			int digit3Minute = minutes % 10;
			int digit4Minute = minutes / 10;
			
			digitUISprite[0].spriteName = "" + digit1Second.ToString();
			digitUISprite[1].spriteName = "" + digit2Second.ToString();
			digitUISprite[2].spriteName = "" + digit3Minute.ToString();
			digitUISprite[3].spriteName = "" + digit4Minute.ToString();
			
			//offset positionX of digitUISprite[0] and digitUISprite[2]
			if(digitUISprite[0].spriteName == "1"){
				digitUISprite[0].transform.setLocalPositionX(54f);
			}
			else{ digitUISprite[0].transform.setLocalPositionX(74f); }
			
			if(digitUISprite[2].spriteName == "1"){
				digitUISprite[2].transform.setLocalPositionX(-24f);
			}
			else{ digitUISprite[2].transform.setLocalPositionX(-4f); }
			
			
			//Update FeverTime Gage
			if(percentage < 20){ feverTimeGageUpdate(0); } // ~0
			else if(percentage < 40){ feverTimeGageUpdate(1);}// ~20
			else if(percentage < 60){ feverTimeGageUpdate(2); }// ~40
			else if(percentage < 80){ feverTimeGageUpdate(3); }// ~60
			else if(percentage < 100){ feverTimeGageUpdate(4); }// ~80
			else{	feverTimeGageUpdate(5); }//100%を超えたら
		}
	}
	
	void feverTimeGageUpdate(int receivePercentage){
		feverTimeGageUISprite.spriteName =  "FeverTimeGage/main_gage_fever_" + receivePercentage.ToString();
	}
	
	void hideGageAndTimer(){
		Timer.SetActive(false);
		feverTimeGageUpdate(5);
	}
	
	void feverTimeIsEnd(){
		Timer.SetActive(true);
		for(int i = 0; i < 4; i++){
			if(digitUISprite[i] == null){
				digitUISprite[i] = GameObject.Find("Digit" + (i + 1).ToString()).GetComponent<UISprite>();
			}
			digitUISprite[i].enabled = true;
		}
		
		if(colonUISprite == null){
				colonUISprite = GameObject.Find("Colon").GetComponent<UISprite>();
		}
		colonUISprite.enabled = true;
	}
	
	#endregion
	
	#region Delegate
	void OnEnable(){
		ScoreManager._pointUpdate += bitmapScoreUpdate;
		ScoreManager._levelUpdate += bitmapLvUpdate;
		ScoreManager._levelUpdate += levelUpEvent;
		ScoreManager._percentageUpdate += bitmapPercentageUpdate;
		ScoreManager._afterLoadGameData += loadDataFromScoreManagerScript;
		ScoreManager._feverTimeGageUpdate += feverTimeGageUpdate;
		ScoreManager._feverTimeScoreUpdate += UIFeverTime;
		ScoreManager._getNewChickEvent += showPlate;
		UI_FeverTime._feverTimeTimingEnd += feverTimeIsEnd;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
		ScoreManager._pointUpdate -= bitmapScoreUpdate;
		ScoreManager._levelUpdate -= bitmapLvUpdate;
		ScoreManager._levelUpdate -= levelUpEvent;
		ScoreManager._percentageUpdate -= bitmapPercentageUpdate;
		ScoreManager._afterLoadGameData -= loadDataFromScoreManagerScript;
		ScoreManager._feverTimeGageUpdate -= feverTimeGageUpdate;
		ScoreManager._feverTimeScoreUpdate -= UIFeverTime;
		ScoreManager._getNewChickEvent -= showPlate;
		UI_FeverTime._feverTimeTimingEnd -= feverTimeIsEnd;
	}
	#endregion
	
	void Update(){
		
	}
	
	
	/*
	//test Score Update
	float timer = 0f;
	int hoge = 1;
	
	void Update () {
		timer += Time.deltaTime;
		if(timer >0.9f){
		hoge += 1;
		bitmapLvUpdate(hoge);
//		print (score);
		timer = 0f;
		}
	}
	*/
	
}
