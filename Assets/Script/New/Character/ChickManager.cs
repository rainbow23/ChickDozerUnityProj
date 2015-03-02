using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChickManager : SingletonMonoBehaviour<ChickManager>{
	public static GameObject[] allChickObjs = new GameObject[36];
	ScoreManager ScoreManagerScript;
	
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
	void loadChickPrefab () {
		//PrefabのChickを配列にいれ、数字順に並べる  //Allchicks and Eggs num =36
		allChickObjs = Resources.LoadAll("ChickAnimUnityAndEggs",typeof(GameObject))
			.Cast<GameObject>()
			.OrderBy(t => {
				string chickIndexString = t.name.Substring(t.name.Length -3);
				int chickIndexInt = int.Parse(chickIndexString);
				return chickIndexInt;
			})
			.ToArray();
	}
	
	
	
	//札束
	GameObject loadWadMoney;
	
	void Start(){
		ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		//札束
		loadWadMoney = Resources.Load("WadMoney", typeof(GameObject)) as GameObject;
	}
	
	int createChickNum;
	
	
	
	
	void createChick(Vector3 touchPos){
		
		//札束をタップした位置に落とす
		if(ScoreManagerScript.createWadMoney){
			GameObject createWadMoney;
						
			//createWadMoney = Instantiate(loadWadMoney, touchPos, Quaternion.identity) as GameObject;
			//createWadMoney.name = loadWadMoney.name;
		}
		else{
		
			//print ("touchPos: " + touchPos);
			
			//もしNGUIイベントが無かったら作る
			if(ScoreManager.touchNguiButtonFlag){ return; }
			
			createChickNum =  selectChickByLevel();
			
			GameObject createChick;
			createChick = Instantiate(allChickObjs[createChickNum], 
													touchPos, 
													Quaternion.identity) as GameObject;
			createChick.name = allChickObjs[createChickNum].name;
			createChick.rigidbody.angularDrag = 80f;
		}
	}
	
	int level = 1;
	void createChickPercentageByLevel(){
		/*
		posY_steps[0] = 0;//50%
		posY_steps[1] = 0;
		posY_steps[2] = 0;
		posY_steps[3] = 0;	
		posY_steps[4] = 0;//90%
		posY_steps[5] = 4;//100%
		
		//posY_steps
		int posY_stepsRandomResult = GetRandomByWeight(posY_steps, 6); //0,1,2,3,4,5
		//print ("posY_stepsRandomResult: " + posY_stepsRandomResult);
		setPosY *= (posY_stepsRandomResult + 5)*0.1f; //set percent
		*/
		
		/*
		例
		（レベル４）
		ひよこ種類：１,    ２,     ３,    ４, 
		確率：       3/6,  2/6,  1/6,   3%
		*/
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
		float startPosX = -1.6f;
		float startPosZ = -5f;
		float startPosY = 5.8f;
		int randomX = Random.Range(0, 4);// 0 ~ 3
		createChickNum =  selectChickByLevel();
		
		GameObject chick;
		float posX = startPosX - (randomX * 1.7f);
		chick = Instantiate(ChickManager.allChickObjs[createChickNum]) as GameObject;
		chick.transform.position = new Vector3(posX, startPosY, startPosZ);
		chick.name = ChickManager.allChickObjs[createChickNum].name;
		//Debug.Break();
	}
	
	float feverTimer = 0f;
	float createTiming = 1.0f;
	void Update () {
		if(ScoreManager.feverTimeFlag){
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
