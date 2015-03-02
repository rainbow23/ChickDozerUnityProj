using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterPool : SingletonMonoBehaviour<CharacterPool>{
	
	public delegate void chickHandler();
	public static event chickHandler _createChick;
	
	public static bool objectPoolMode = true;
	public bool _objectPoolMode;
	List<Transform> chickPoolObj = new List<Transform>();
	public static GameObject[] allChickObjs = new GameObject[36];
	ScoreManager ScoreManagerScript;
	
	UISprite HowToUISceneUISprite;
	
	public void Awake (){
		if (this != Instance) {
			Destroy(this);
			Destroy (this.gameObject);
			return;
		}
		else{
			DontDestroyOnLoad (this.gameObject);
			loadChickPrefab();
		}
	}
	
	int[] hoges = new int[7];
	void loadChickPrefab() {
		//PrefabのChickを配列にいれ、数字順に並べる  //Allchicks and Eggs num =36
		allChickObjs = Resources.LoadAll("ChickNewUv",typeof(GameObject))
												.Cast<GameObject>()
												.OrderBy(t => {
													string chickIndexString = t.name.Substring(t.name.Length -3);
													int chickIndexInt = int.Parse(chickIndexString);
													return chickIndexInt;
												})
												.ToArray();
	}
	
	void Start(){
		ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		HowToUISceneUISprite = GameObject.Find("HowToUIScene").GetComponent<UISprite>();
		
		GameObject PoolChick = GameObject.Find("PoolChick");
		for(int num = 0; num < PoolChick.transform.childCount; num++){
			Transform childObj = PoolChick.transform.GetChild(num);
			chickPoolObj.Add(childObj);  //add List<Transform>
		}
		
		if(_objectPoolMode){
			objectPoolMode = true;
		}
		else{ objectPoolMode = false; }
	}
	
	int createChickNum;
	
	void createChick(Vector3 touchPos){
		//print ("touchPos: " + touchPos);
		//もしNGUIイベントが無かったら作る
		if(ScoreManager.touchNguiButtonFlag){ return; }
			
		_createChick();// Delegate to SEManager.cs
		
		createChickNum =  selectChickByLevel();
			
		if(objectPoolMode){
			var pool = Pool.GetObjectPool(allChickObjs[createChickNum]);
			// インスタンス生成
			GameObject obj = pool.GetInstance();
			//rigidbody.centerOfMassが変わっていたのでタップ時初期化
			obj.rigidbody.centerOfMass = new Vector3(0f, -0.5f, -0.7f);
			
			obj.transform.position = touchPos;
			obj.transform.setEulerAnglesX(-30f);
			obj.name = allChickObjs[createChickNum].name;
			//obj.SendMessage("initialize");
			//Debug.Break();
		}
		else{
			GameObject obj;
			obj = Instantiate(allChickObjs[createChickNum], 
														touchPos, 
														Quaternion.Euler(-30f, 0f, 0f)) as GameObject;
			obj.name = allChickObjs[createChickNum].name;
			obj.rigidbody.angularDrag = 60f;
		}
	}
	
	int[] createChickPer = new int[33];
	
	int selectChickByLevel(){
		int maxChickNum = ScoreManager.levelNum;
		//
		if(maxChickNum == 33){ maxChickNum = 32;}
		//int[] createChickPer = new int[receivePoint];
		
		int arrayNum = maxChickNum ;
		for(int a =0; a < maxChickNum; a++){
			//print ("配列: " + a);
			createChickPer[a] = arrayNum;
			//print ("createChickPer[ " + a + "]:  " + createChickPer[a]);
			arrayNum -= 1;
		}
		
		//今のレベル以上なら0を入れる　
		for(int restNum = maxChickNum; restNum < 33; restNum++){
			createChickPer[restNum] = 0;
		}
		
		int result = GetRandomByWeight(createChickPer, 33);
		
		/*
		for (int i = 0; i < 33; i++){
			print("createChickPer[" + i + "]: " + createChickPer[i]);
		}
		*/	
		
		int choice = Random.Range(1,101);
		//3%で一番新しいひよこを出す
		if(choice ==1 || choice ==2 || choice ==3){
			//print ("3%で一番新しいひよこ");
			return maxChickNum;
		}
		//97%
		else{
			return result;
		}
	}
	
	
	
	void createChickInFeverTimeMode(){
		_createChick();// Delegate to SEManager.cs
		
		float startPosX = -1.6f;
		float startPosZ = -5f;
		float startPosY = 5.8f;
		int randomX = Random.Range(0, 4);// 0 ~ 3
		createChickNum =  selectChickByLevel();
		
		GameObject chick;
		float posX = startPosX - (randomX * 1.7f);
		chick = Instantiate(CharacterPool.allChickObjs[createChickNum]) as GameObject;
		chick.transform.position = new Vector3(posX, startPosY, startPosZ);
		chick.name = CharacterPool.allChickObjs[createChickNum].name;
		//Debug.Break();
	}
	
	
	
	float feverTimer = 0f;
	float createTiming = 1.0f;
	void Update () {
		//HowToが表示されていないならばひよこを作る
		if(ScoreManager.feverTimeFlag && HowToUISceneUISprite.transform.localPosition.x != 0f){
			feverTimer += Time.deltaTime;
			if(feverTimer > createTiming){
				createChickInFeverTimeMode();
				feverTimer = 0f;
			}
		}
	}
	
	#region random
	int GetRandomByWeight(int[] hoge,int count){
		int result = 0;
		int sum = 0;

		for (int i = 0; i < count ; i++){
			sum += hoge[i];
		}
		
		int t = Random.Range(0,sum);
		for(int i = 0 ; i < count ; i++){
			if(t < hoge[i]){
				result = i;
				break;
			}
			t -= hoge[i];
		}
		return result; //
	}
	#endregion
	
	
	#region Delegate
	void OnEnable(){
		ScoreManager._touchPositionFromTouchControl += createChick;
		//TouchControl._touchPosition += createChick;
//		UI_GameScene._feverTimeEvent += feverTimeStart;
//		UI_FeverTime._feverTimeTimingEnd += feverTimeEnd;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
		ScoreManager._touchPositionFromTouchControl -= createChick;
		//TouchControl._touchPosition -= createChick;
//		UI_GameScene._feverTimeEvent -= feverTimeStart;
//		UI_FeverTime._feverTimeTimingEnd -= feverTimeEnd;
	}
	#endregion
}
