using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>{
	#region variable
	PlugInObject PlugInObjectScript;
	public delegate void scoreHandler(int num);
	public static event scoreHandler _percentageUpdate;
	public static event scoreHandler _levelUpdate;
	
	public delegate void pointHandler(int point);
	public static event pointHandler _pointUpdate;
	public static event pointHandler _feverTimeGageUpdate;
	
	public delegate void eventHandler();
	public static event eventHandler _afterLoadGameData;
	public static event eventHandler _getNewChickEvent;
	
	public delegate void feverTimeHandler(int countSecond, int countMinusSecond);
	public static event feverTimeHandler _feverTimeScoreUpdate; 
	
	public delegate void touchPosHandler(Vector3 _touchToWorldPos);
    public static event touchPosHandler  _touchPositionFromTouchControl;
	
	public delegate void hudHandler(int chickScore, Transform chickHitPosFromBasketManager);
    public static event hudHandler  _hudChickPointIsStart;
	
	public static int point = 2000;
	public static int totalPoint = 0;
	public static int score = 0;
	public static int levelNum = 1;
	public static int nextLevelPercentage;
	public static int tapCount{private set; get;}
	static int countChickAtSave = 0;
	static int chickHitBasketCount = 0;
	static int feverTimeCount = 0;
	public static int diffSecond = 0;
	public static int countFeverTimeEvent = 0; // use UI_GameScene.cs
//	public static int feverTimeGage{set; get;} // not use
//	public static float feverTimer; //not use
	//use void pointUpdateByTouch
	public static bool touchNguiButtonFlag = false;
	public  bool autoCreateMode = true;
	//public static bool loadSceneFromCollection = false;
	
	public Dictionary<string, int> chickScoreDic{private set; get;}
	public Dictionary<string, int> collectionChickDic{private set; get;}
	 int[]getChickNumber = new int[33];
	#endregion
	
	//test
	public bool PlayerPrefsDeleteAll = false;
	public bool createWadMoney = false;
	
	
	public void Awake (){
		if (this != Instance) {
			Destroy(this);
			Destroy (this.gameObject);
			return;
		}
		else{
			//DontDestroyOnLoad (this.gameObject);
		}
		defineChickScoreList();
	}
	
	void Start(){
		//PlayerPrefs.SetInt("volumeSwitch",0);
		//Time.timeScale =20f;
		if(PlayerPrefsDeleteAll){ PlayerPrefs.DeleteAll(); }
		#if UNITY_EDITOR
		#elif UNITY_IPHONE
		PlugInObjectScript = GameObject.Find("webViewGameObject").GetComponent<PlugInObject>();//Plugin
		#endif
		
		totalPoint = PlayerPrefs.GetInt("totalPoint");
		//print ("Load totalPoint: " + totalPoint);
		
		touchNguiButtonFlag = false;
		
		if(PlayerPrefs.GetInt("FirstStartGame") == 0){
			print ("初めてゲームスタート");
			float posZ;// = -2f;
			float posX;// = -6.6f;
			
			GameObject chick;
			for(int z = 0; z < 3; z ++){//offset Z
				posZ = -5f + (z * 2f);
				for(int i = 0; i < 4; i++){//offset X
					posX = -1.6f - (i * 1.7f);
					chick = Instantiate(CharacterPool.allChickObjs[0]) as GameObject;
					chick.transform.position = new Vector3(posX, 0.4f, posZ);
					chick.name = CharacterPool.allChickObjs[0].name;
				}
			}
			
			PlayerPrefs.SetInt("FirstStartGame", 1);
			_afterLoadGameData(); //Delegate use UI_GameScene.cs
		}
		else{
			// 奇数のまま数字がかえってきたらアプリ強制終了またはコレクション画面から遷移後なので保存したひよこを読み込む
			//1,3,5,7,9,
			if(PlayerPrefs.GetInt("ApplicationPauseCount") % 2 != 0){
				loadGameData();
				loadChick();
				
				_afterLoadGameData(); //Delegate use UI_GameScene.cs
//				_feverTimeScoreUpdate(countSecond, countMinusSecond); //Delegate to UI_Game_Scene.cs

				PlayerPrefs.SetInt("ApplicationPauseCount", 0);				
			}	
		}
	}
	
	void defineChickScoreList(){
		chickScoreDic = new Dictionary<string, int>();
		for(int i =0; i < 33; i ++){
			string num = i.ToString();
			chickScoreDic.Add("chick" + num , i +1);
			//print(num + "chick: " + chickScoreDic["chick" + num]);
		}
	}
	
	
	int[]chickFirstHitBasketArray = new int[33];
	
	//かごに入ったひよこ種類を保存
	void collectionUpdate(int receivedChickNumByHitBasket){	
		// -1はCollectionシーン読み込み時に設定される
		if(chickFirstHitBasketArray[receivedChickNumByHitBasket] == 0){
		chickFirstHitBasketArray[receivedChickNumByHitBasket] = 1;
		_getNewChickEvent(); //Delegate UI_GameScene.cs
		}
	}
	
	//Save chickFirstHitBasketDic to PlayerPrefs
	void saveChickFirstHitBasketFlagToDisk(){
		int chickNum = 0;
		string chikNumString;
		
		while(chickNum < 33){
			chikNumString = chickNum.ToString();
			PlayerPrefs.SetInt("chickHitFirst" + chikNumString, chickFirstHitBasketArray[chickNum]);
//			print("Save: " + PlayerPrefs.GetInt("chickHitFirst" + chikNumString));
			chickNum += 1;
		}
	}
	
	void loadChickFirstHitBasketFlagFromDisk(){
		int chickNum = 0;
		string chikNumString;
		
		while(chickNum < 33){
			chikNumString = chickNum.ToString();
			chickFirstHitBasketArray[chickNum] = PlayerPrefs.GetInt("chickHitFirst" + chikNumString);
			chickNum += 1;
		}
	}
	
	
	void scoreUpdate(int receivedChickNumByHitBasket, Transform receivedChickTransform){
		//add score from  received chickNum
		string num = receivedChickNumByHitBasket.ToString();
		int chickScore = chickScoreDic["chick" + num];
//		print("chickScore: " + chickScore);
		
		score += chickScore;
		
		int addPoint = chickScore * 10;
		
		//print ("point: " + point);
		point += addPoint;
		
		//use totalScore in GameCenter
		totalPoint += addPoint;
		
		//print ("addPoint: " + addPoint);
		//print ("point: " + point);
		
		if(point > 9999){
				point = 9999;
		}
		
		_hudChickPointIsStart(addPoint, receivedChickTransform); //Delegate to HudManager.cs
		//print ("point += chickScore * 10:  " + point);
		chickHitBasketCount += 1;
		//Debug.Break();
		
		//calculate percentage and levelCount to use score
		checkLevelUpdate(score);
		percentageUpdate(score);
		//judgeFeverTimeEvent(chickHitBasketCount); タイマー実装を使うため保留
		_pointUpdate(point); //Delegate
	}
	
	void checkLevelUpdate(int receiveScore){
		//レベルが上がったら
		if(score > nextLevelScore(levelNum)){
			//前のスコアを引き今のレベルの得点だけにする
//			print ("score: " + score);
//			print("nextLevelScore(levelNum): " + nextLevelScore(levelNum));
			score = score - nextLevelScore(levelNum);
//			print ("score - nextLevelScore(levelNum): " + score);
			levelNum += 1;
			_levelUpdate(levelNum); //Delegate
		}
	}
	
	void percentageUpdate(int receiveScore){
		float toNextLevelPercentage = (float)receiveScore / (float)nextLevelScore(levelNum);
		toNextLevelPercentage = toNextLevelPercentage* 100;
		toNextLevelPercentage = Mathf.Floor(toNextLevelPercentage);
//		print ("toNextLevelPercentage: " + toNextLevelPercentage);
		int digit1 = (int)toNextLevelPercentage % 10;
		int digit2 = (int)toNextLevelPercentage / 10;
		
		//print ("digit1: " + digit1);
		//print ("digit2: " + digit2);
		//if 16 > 20
		if(digit1 > 5){
			digit2 += 1;
		}
		//if 96 > 90
		if(digit2 == 10){
				digit2 = 9;
		}
		digit2 *= 10;
		digit1 = 0;
		
		nextLevelPercentage = (int)(digit2 + digit1);
		
		switch(nextLevelPercentage){
			case 10:
				nextLevelPercentage = 0;
				break;
			case 30:
				nextLevelPercentage = 20;
				break;
			case 50:
				nextLevelPercentage = 40;
				break;
			case 70:
				nextLevelPercentage = 60;
				break;
			case 90:
				nextLevelPercentage = 80;
				break;
		}
		//print ("digit2 + digit1: " + nextLevelPercentage);
		_percentageUpdate(nextLevelPercentage);//Delegate
	}
	//dictionary?
	int nextLevelScore(int currentLevel){
		int nextLevelScore;
			switch(currentLevel){
			case(0):
				return 0;
			case(1):
				nextLevelScore = 10;				
				break;
			case(2):
				nextLevelScore = 21;				
				break;
			case(3):
				nextLevelScore = 42;
				break;
			case(4):
				nextLevelScore = 87;
				break;
			case(5):
				nextLevelScore = 167;
				break;
			case(6):
				nextLevelScore = 318;
				break;
			case(7):
				nextLevelScore = 448;
				break;
			case(8):
				nextLevelScore = 599;
				break;
			case(9):
				nextLevelScore = 771;
				break;
			case(10):
				nextLevelScore = 964;
				break;
			case(11):
				nextLevelScore = 1254;
				break;
			case(12):
				nextLevelScore = 1623;
				break;
			case(13):
				nextLevelScore = 2099;
				break;
			case(14):
				nextLevelScore = 2704;
				break;
			case(15):
				nextLevelScore = 3455;
				break;
			case(16):
				nextLevelScore = 4564;
				break;
			case(17):
				nextLevelScore = 5907;
				break;
			case(18):
				nextLevelScore = 7500;
				break;
			case(19):
				nextLevelScore = 9356;
				break;
			case(20):
				nextLevelScore = 13241;
				break;
			case(21):
				nextLevelScore = 17972;
				break;
			case(22):
				nextLevelScore = 23818;
				break;
			case(23):
				nextLevelScore = 31017;
				break;
			case(24):
				nextLevelScore = 39781;
				break;
			case(25):
				nextLevelScore = 50299;
				break;
			case(26):
				nextLevelScore = 62736;
				break;
			case(27):
				nextLevelScore = 77241;
				break;
			case(28):
				nextLevelScore = 98417;
				break;
			case(29):
				nextLevelScore = 155630;
				break;
			case(30):
				nextLevelScore = 224735;
				break;
			case(31):
				nextLevelScore = 308648;
				break;
			case(32):
				nextLevelScore = 410263;
				break;
			default:
					return 30000000;
				break;
			}
		return nextLevelScore;
	}
	
	int tapPoint(int currentLevel){
		int tapPoint;
		switch(currentLevel){
			case(1):
				tapPoint = 4;				
				break;
			case(2):
			case(3):
				tapPoint = 8;
				break;
			case(4):
			case(5):
				tapPoint = 12;
				break;
			case(6):
				tapPoint = 16;
					break;
			case(7):
				tapPoint = 20;
				break;
			case(8):
				tapPoint = 28;
					break;
			case(9):
				tapPoint = 36;
				break;
			case(10):
				tapPoint = 44;
					break;
			case(11):
				tapPoint = 52;
					break;
			case(12):
				tapPoint = 60;
					break;
			case(13):
				tapPoint = 68;
				break;
			case(14):
				tapPoint = 76;
				break;
			case(15):
				tapPoint = 84;
				break;
			case(16):
				tapPoint = 92;
				break;
			case(17):
				tapPoint = 100;
				break;
			case(18):
				tapPoint = 108;
				break;
			case(19):				
				tapPoint = 116;
				break;
			case(20):
				tapPoint = 124;
				break;
			case(21):
				tapPoint = 132;
				break;
			case(22):
				tapPoint = 140;
				break;
			case(23):
				tapPoint = 148;
				break;
			case(24):
				tapPoint = 156;
				break;
			case(25):
				tapPoint = 164;
				break;
			case(26):
				tapPoint = 172;
				break;
			case(27):
				tapPoint = 180;
				break;
			case(28):
				tapPoint = 188;
				break;
			case(29):
				tapPoint = 196;
				break;		
			case(30):		
				tapPoint = 204;
				break;		
			case(31):
				tapPoint = 212;
				break;
			case(32):
				tapPoint = 220;
				break;
			case(33):
				tapPoint = 228;
				break;
			default:
					return 228;
				break;
		}
//		print ("tapPoint: " + tapPoint);
		return tapPoint;
	}
	
	void pointUpdateByTouch(Vector3 touchPos){ 
		if(touchNguiButtonFlag){return;}
		tapCount += 1;
		
		//create chick
		if(point < 5){ return; }
		else{
			//print("point: " + point);
			point -= tapPoint(levelNum);
			//print("point -= tapPoint(levelNum): " + point);
			if(point < 0){ 
				point = 0; 
			}
		}
		_touchPositionFromTouchControl(touchPos); //delegate to ChickManager.cs
		_pointUpdate(point); //Delegate
	}
	
	//message from iOS
	void getChickPointsFromInAppPurchase(string getPoint){		
		point += int.Parse(getPoint);
		if(point > 9999){
				point = 9999;
		}
		_pointUpdate(point); //Delegate
	}
	
	int getPauseCount;
	void OnApplicationPause(){
		getPauseCount = PlayerPrefs.GetInt("ApplicationPauseCount");
		getPauseCount += 1;
				
		PlayerPrefs.SetInt("ApplicationPauseCount", getPauseCount);
		PlayerPrefs.Save();
		
		//バックグラウンドに回ったらシーンの情報を保存する
		//1,3,5,7,9,
		if(PlayerPrefs.GetInt("ApplicationPauseCount") % 2 != 0){
//			print ("Enter BackGround: ");
			saveGameData();
			//PlayerPrefs.DeleteAllがSaveDataにあるので復元
			PlayerPrefs.SetInt("ApplicationPauseCount", getPauseCount);
			PlayerPrefs.Save();
		}
		else{
//			print ("Back Game");
			loadGameData();
			_afterLoadGameData(); //Delegate use UI_GameScene.cs
			//ShowAddRanking
			
			#if UNITY_EDITOR
			#elif UNITY_IPHONE
				PlugInObjectScript.ShowAddRanking();
			#endif
			//Time.timeScale = 0f;
		}
		
	}
	// from iOS message if bead 
	void timeStart(){
		Time.timeScale = 1f;
	}
	
	
	#region SaveData 
	void saveGameData(){
		//PlayerPrefs.DeleteAll();
	
		PlayerPrefs.SetInt("totalPoint", totalPoint);
		
//		PlayerPrefs.SetInt("volumeSwitch", SoundManager.volumeSwitch);
		//print("volumeSwitch: " + PlayerPrefs.GetInt("volumeSwitch"));
		//Debug.Break();
		
		PlayerPrefs.SetFloat("FeverTimeCountSecond", UI_FeverTime.FeverTimeCountSecond);
		//print ("Save: FeverTimeCountSecond" +  PlayerPrefs.GetInt("FeverTimeCountSecond"));
		
		saveChickFirstHitBasketFlagToDisk();
		PlayerPrefs.SetInt("FirstStartGame", 1);
		
		//playerprefsデータを保存
		PlayerPrefs.SetInt("levelNum", levelNum);
		PlayerPrefs.SetInt("score", score);
		PlayerPrefs.SetInt("point", point);
		PlayerPrefs.SetInt("nextLevelPercentage", nextLevelPercentage);
		PlayerPrefs.SetInt("chickHitBasketCount", chickHitBasketCount);
		
		//feverTime各パラメーター
		PlayerPrefs.SetInt("countSecond", countSecond);
		PlayerPrefs.SetInt("countMinusSecond", countMinusSecond);
		PlayerPrefs.SetInt("feverTimeFlag", Convert.ToInt32(feverTimeFlag));
		PlayerPrefs.SetInt("countFeverTimeEvent", countFeverTimeEvent);
		
		countChickAtSave = 0;
		storeTime();
		
		GameObject[] allChickInScene;
		allChickInScene = GameObject.FindGameObjectsWithTag("GameObjectsInScene") as GameObject[];
		
		foreach(GameObject eachChick in allChickInScene){
			countChickAtSave += 1;
			string setKeyChickNum = eachChick.name.Substring(eachChick.name.Length - 3);
			setKeyChickNum += countChickAtSave.ToString();
			
			//store position 
			string setKeyChickPos =  setKeyChickNum + "pos";
			Vector3 eachChickPosition = eachChick.transform.position;
			PlayerPrefsPlus.SetVector3(setKeyChickPos, eachChickPosition);
//			print ("Store: eachChickPosition: " + eachChickPosition);
			
			//store rotation
			string setKeyChickRotation =  setKeyChickNum + "rotation";
			Vector3 eachChickRotation = eachChick.transform.rotation.eulerAngles;
			PlayerPrefsPlus.SetVector3(setKeyChickRotation, eachChickRotation);
//			print ("Store: eachChickRotation: " + eachChickRotation);
		}
		
		PlayerPrefs.SetInt("countChickAtSave", countChickAtSave);
		PlayerPrefs.Save();
//		print("Save countChick: " + countChickAtSave);
		//print("Save feverTimer: " + feverTimer);
	}
	
	void storeTime(){
		string storeStringTicks = DateTime.Now.Ticks.ToString();
		//print ("storeStringTicks: " + storeStringTicks);
		
		//長いので秒で始まっているところで切る
		storeStringTicks = storeStringTicks.Substring(0, storeStringTicks.Length -7);
//		print ("Substring: StringTicks: " + storeStringTicks);
		PlayerPrefs.SetString("DateTime.Now.Ticks", storeStringTicks);
	}
	#endregion
	
	
	int count;
	#region LoadData
	void loadGameData(){
		//volumeSwitch = PlayerPrefs.GetInt("volumeSwitch");
		//print("volumeSwitch: " + PlayerPrefs.GetInt("volumeSwitch"));
		
		UI_FeverTime.FeverTimeCountSecond = PlayerPrefs.GetFloat("FeverTimeCountSecond", UI_FeverTime.FeverTimeCountSecond);
		
		countFeverTimeEvent = PlayerPrefs.GetInt("countFeverTimeEvent");
		//print ("Load countFeverTimeEvent: " + countFeverTimeEvent);
		
		levelNum = PlayerPrefs.GetInt("levelNum", levelNum);
		score = PlayerPrefs.GetInt("score", score);
		point = PlayerPrefs.GetInt("point", point);
		nextLevelPercentage = PlayerPrefs.GetInt("nextLevelPercentage", nextLevelPercentage);
		countChickAtSave = PlayerPrefs.GetInt("countChickAtSave", countChickAtSave);
		print ("countChickAtSave: " + countChickAtSave);
		chickHitBasketCount = PlayerPrefs.GetInt("chickHitBasketCount", chickHitBasketCount);
//		feverTimeGage = PlayerPrefs.GetInt("feverTimeGage", feverTimeGage); not use
		//feverTimeCount = PlayerPrefs.GetInt("feverTimeCount", feverTimeCount);
//		feverTimer = PlayerPrefs.GetFloat("feverTimer", feverTimer);//使ってない？
		loadChickFirstHitBasketFlagFromDisk();
		
		//かごに入ったひよこ種類を復元
		int num = 0;
		while(num < 33){
			string numString = num.ToString();
			getChickNumber[num] = PlayerPrefs.GetInt("chick" + numString);
//			print("chick: " + getChickNumber[num]);
			//Debug.Break();
			num += 1;
		}
		
		//ゲームから離れているときの時間を取得
		loadDiffTime();
		
		//feverTime各パラメーター
		countSecond = PlayerPrefs.GetInt("countSecond", countSecond);
		countMinusSecond = PlayerPrefs.GetInt("countMinusSecond", countMinusSecond);
		
		countSecond += diffSecond;
		countMinusSecond -= diffSecond;
		
		feverTimeFlag = (PlayerPrefs.GetInt("feverTimeFlag")  == 0)? false: true;		
		
		//3秒に１ポイント追加
		if(point < 9999){
			point += (diffSecond /3 );
			if(point > 9999){
				point = 9999;
			}
		}
	}
		
	#region load Chick
	void loadChick(){
		//load chick when stored number matchs  registered chick
		foreach(GameObject eachObj in CharacterPool.allChickObjs){
			//print ("eachObj.name: " + eachObj.name);
			string ChickNum = eachObj.name.Substring(eachObj.name.Length - 3);			
			
			//ゲームが終わる前に保存したシーンのひよこの数でループ
			for(int i = 1; i < countChickAtSave +1; i++){
				string storedKey = ChickNum + i.ToString();
				string storedKeyChickPos = storedKey + "pos";
				string storedKeyChickRotation = storedKey + "rotation";
					
				
				//keyが存在しない場合、(0,0,0)を返すのでこれを省く
				if(PlayerPrefsPlus.GetVector3(storedKeyChickPos) != new Vector3(0f,0f,0f)){
					Vector3 storedChickPos = new Vector3(0, 0, 0);
					Vector3 storedChickRotation= new Vector3(0, 0, 0);
					
					//Recovery chick Position////////////////////////////////////////////////////
					storedChickPos = PlayerPrefsPlus.GetVector3(storedKeyChickPos);
//					print("LoadKey: " + storedKeyChickPos +", Pos: "+ storedChickPos);
					
				
					//Recovery chick Rotation////////////////////////////////////////////////////
					if(PlayerPrefsPlus.GetVector3(storedKeyChickRotation) != new Vector3(0f,0f,0f)){
						storedChickRotation = new Vector3(0, 0, 0);					
						//各ひよこrotationを復元させる:
						storedChickRotation = PlayerPrefsPlus.GetVector3(storedKeyChickRotation);
//						print ("LoadKey: " + storedKeyChickRotation + ", Rotation: "+  storedChickRotation);
					}
					
					//ひよこを作る
					GameObject instantiateObj = Instantiate(eachObj, storedChickPos, Quaternion.Euler(storedChickRotation)) as GameObject;
					instantiateObj.name = eachObj.name;	
				}
				
				//initialize
				PlayerPrefsPlus.SetVector3(storedKeyChickPos, new Vector3(0f, 0f, 0f));
				PlayerPrefsPlus.SetVector3(storedKeyChickRotation, new Vector3(0f, 0f, 0f));
				
			}	
		}
		//Debug.Break();
	}
	#endregion
	
	void loadDiffTime(){
		long nowTicksInt;
		long storedTicksInt;
		
		//今の時間を取得
		string nowTicks = DateTime.Now.Ticks.ToString();
		//print ("nowTicks: " + nowTicks);
		//長いので秒で始まっているところで切る
		nowTicks = nowTicks.Substring(0, nowTicks.Length -7);
		nowTicksInt = Convert.ToInt64(nowTicks);
		//print ("nowTicksInt: " + nowTicksInt);
		
		//保存した時間を取得
		string storedTicks = PlayerPrefs.GetString("DateTime.Now.Ticks");
		storedTicksInt = Convert.ToInt64( storedTicks);
		//print ("storedTicksInt: " + storedTicksInt);
		
		//差分を計算
		diffSecond = (int)(nowTicksInt - storedTicksInt);
		
		//fadeInOut合計３秒なので引く
		diffSecond -= 3;
	}
	#endregion
	
	public static bool feverTimeFlag = false;
	//static float percentage;
	public static int limitMinute = 5;
	public static int countSecond{private set; get; }
	public static int countMinusSecond = limitMinute * 60;
	
	//UI feverTime 
	public void feverTimerUpdate(){
		if(feverTimeFlag){return;}
		countSecond += 1;
		countMinusSecond -= 1;
		
		// Send Message to UI  
		//_feverTimeScoreUpdate(countSecond, countMinusSecond); //Delegate to UI_Game_Scene.cs
	}
	
	void feverTimeIsStart(){
		countFeverTimeEvent += 1;
		feverTimeFlag = true;
		
		switch(countFeverTimeEvent){
			case 0:
				limitMinute = 5;
				break;
			case 1:
				limitMinute = 10;
				break;
			case 2:
				limitMinute = 15;
				break;
			case 3:
				limitMinute = 20;
				break;
			case 4:
				limitMinute = 25;
				break;
			default:
				limitMinute = 30;
				break;
		}
		//print ("countFeverTimeEvent: " + countFeverTimeEvent);
	}
	
	void feverTimeIsEnd(){
		countSecond = 0;
		countMinusSecond = limitMinute * 60;
		feverTimeFlag = false;
	}
	
	//3秒に１ポイント追加
	float pointTimer = 0f;
	void addPointByTime(){
		if(Application.loadedLevelName == "Title" || Application.loadedLevelName == "Collection" ){return;}
		point += 1;
		if(point > 9999){
			point = 9999;
		}
		_pointUpdate(point); // Delegate
	}
	
	//use  When create chick : ChickManager.cs
	public void touchNguiButton(){
		touchNguiButtonFlag = true;
	}
	
	public void arrowCreateChick(){
		touchNguiButtonFlag = false;
	}
	
	float secTimer = 0f;
	float autoCreateTimer = 0f;
	void Update(){
		if(autoCreateMode){
			float hoge = UnityEngine.Random.Range(-6.49f, -1.6f);
			autoCreateTimer += Time.deltaTime;
			if(autoCreateTimer > 1.0f){
				_touchPositionFromTouchControl(new Vector3(hoge, 3.5f, -3.8f));
				autoCreateTimer = 0f;
			}	
		}
			
		pointTimer += Time.deltaTime;
		if(pointTimer > 3.0f){
			addPointByTime();
			pointTimer = 0f;
		}
		
		//Fever Timer
		//if(feverTimeFlag || isMoveTimer == false){return;}
		if(feverTimeFlag){return;}
		secTimer += Time.deltaTime;
		//if(secTimer > 1.0f){
		if(secTimer > 1.0f){
			feverTimerUpdate();
			secTimer = 0f;
		}	
	}
	
	/* not use
	public  bool isMoveTimer = false;
	void moveTimer(){
		isMoveTimer = true;
	}
	*/
	
	#region Delegate
	void OnEnable(){
//		BasketManager._hitChickBasket2 += scoreUpdate;
//		BasketManager._hitChickBasket += collectionUpdate;
//		UI_GameScene._startTransition += saveGameData;
//		TouchControl._touchPosition += pointUpdateByTouch;
//		UI_GameScene._feverTimeEvent +=feverTimeIsStart;
//		UI_FeverTime._feverTimeTimingEnd += feverTimeIsEnd;

		//UI_GameScene._isMoveChick += moveTimer;
		//SoundManager._isMoveTimer += moveTimer;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
//		BasketManager._hitChickBasket2 -= scoreUpdate;
//		BasketManager._hitChickBasket -= collectionUpdate;
//		UI_GameScene._startTransition -= saveGameData;
//		TouchControl._touchPosition -= pointUpdateByTouch;
//		UI_GameScene._feverTimeEvent -=feverTimeIsStart;
//		UI_FeverTime._feverTimeTimingEnd -= feverTimeIsEnd;


		//UI_GameScene._isMoveChick -= moveTimer;
		//SoundManager._isMoveTimer -= moveTimer;
	}
	#endregion
	
	
	
}
