using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : MonoBehaviour//SingletonMonoBehaviour<GameController> 
{	
	[HideInInspector]
	public int[] obtainedCharArray;
	private enum State{ firstGet, got};
	private bool isLoadedGame = false;

	DataEvent dataEvent;
	CreateCharManager createCharManager;
	AudioManager audioManager;
	LabelManager labelManager;
	EffectManager effectManager;
	FunctionManager functionManager;

	[HideInInspector]
	public  UnityEvent  UpdatePercentage;
	[HideInInspector]
	public  UnityEvent   UpdateScoreAndLevel;

	//private NotificationObject<int> _score = new NotificationObject<int>(0);
	//public  NotificationObject<int> score { get{ return _score; }}

	public ScoreObject<int, Vector3> scoreObject = new ScoreObject<int, Vector3>();

	private NotificationObject<Vector3> _touchPos = new NotificationObject<Vector3>();
	public NotificationObject<Vector3> touchPos{ get{ return _touchPos; }}

	private NotificationObject<int> _createCharNum = new NotificationObject<int>(0);
	public NotificationObject<int> createCharNum{ get{ return _createCharNum; }}

	void Awake()
	{
		dataEvent = GameObject.Find("DataEvent").GetComponent<DataEvent>();
		Load();

		if(DATA.CheckFirstRun == false)
		{
			Debug.Log("FirstLaunch");
			obtainedCharArray = new int[DATA.ResourcesChickNum];
			DATA.Point = 2000;
			DATA.Level = 1;
			DATA.Score = 0;
			DATA.CheckFirstRun = true;
		}
		else{ 

		}

		Application.targetFrameRate = 60;
		isLoadedGame = true;
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		createCharManager = GameObject.Find("CreateCharManager").GetComponent<CreateCharManager>();
		labelManager = GameObject.Find("LabelManager").GetComponent<LabelManager>();
		functionManager = GameObject.Find("Background").GetComponent<FunctionManager>();
		effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();
	}

	void Start()
	{
		scoreObject.AddListener(AddScore);
		createCharNum.AddListener(audioManager.PlayTouchSE);
		UpdateScoreAndLevel.AddListener(labelManager.updateLabelAction);
		UpdatePercentage.AddListener(labelManager.updatePercentageAction);
		touchPos.AddListener(createCharManager.Create);

		//Destroyで保存すると他のオブジェクトが削除されていてうまく保存できないので
		//ボタンを押した時に保存する
		functionManager.sceneTransitionEvent.AddListener(Save);
	}

	/// <summary>
	/// Modify score and level
	/// </summary>
	void AddScore(int charNum, Vector3 charPos)
	{ 
		int charScore = charNum +1;
		DATA.Score += charScore; 
		DATA.Point += charScore * 10;

		//Debug.Log("obtainedCharArray: " + obtainedCharArray[charNum]);

		//if first time get some kind of chick 
		if(obtainedCharArray[charNum] == 0)
		{
			obtainedCharArray[charNum] = 1;
		}

		//Debug.Log("DATA.Score: " + DATA.Score);

		UpdateLevelPercentage();
		UpdateLevel();

		//After update score and level  you can call  label action
		UpdateScoreAndLevel.Invoke();
		effectManager.ShowScoreEffect(charScore * 10, charPos); 
	}

	void UpdateLevelPercentage()
	{
		int nextLevel = DATA.Level + 1;
		float nextLevelScore = (float)(DATA.NextLevelScore[nextLevel]);
		float score = (float)DATA.Score;
		
		float untilNextScorePercentage = score / nextLevelScore;
		//Debug.Log("untilNextScorePercentage: " + untilNextScorePercentage);
		
		int storeNextLevelPercentage = DATA.NextLevelPercentage;
		
		//整数一桁を返す
		if(untilNextScorePercentage > 0.9 && untilNextScorePercentage < 1.0)
			DATA.NextLevelPercentage = 8;
		else
			DATA.NextLevelPercentage = Mathf.RoundToInt((untilNextScorePercentage) * 10);

		//整数を返す 0,2,4,6,8,10
		if(DATA.NextLevelPercentage % 2 != 0)
			DATA.NextLevelPercentage -= 1;
		
		//割り切れた時を考慮 , It"s level up timing
		if(DATA.NextLevelPercentage >= 10)
		{
			DATA.NextLevelPercentage = 10;
		}

		//Debug.Log("NextLevelPercentage: " + DATA.NextLevelPercentage);

		//更新した時だけラベル更新 またはレベルアップした次にかごに入る時
		if(storeNextLevelPercentage < DATA.NextLevelPercentage || DATA.NextLevelPercentage == 10 || storeNextLevelPercentage == 10)
			UpdatePercentage.Invoke();
	}

	 void UpdateLevel()
	{
		int nextLevel = DATA.Level + 1;
		if(DATA.Score >= DATA.NextLevelScore[nextLevel])
		{
			DATA.Level += 1;
			DATA.Score -= DATA.NextLevelScore[DATA.Level]; //スコアを引き今のレベルの得点だけにする
			Debug.Log("Level Up: " + DATA.Level);
			Debug.Log("Level Upped Score: " + DATA.Score);
		}
	}



	void OnDestroy()
	{
		//Debug.Log("GameController OnDestroy");
		//score.RemoveAllListeners();
		createCharNum.Dispose();
		touchPos.Dispose();
		UpdateScoreAndLevel.RemoveAllListeners();
		UpdatePercentage.RemoveAllListeners();
		functionManager.sceneTransitionEvent.RemoveAllListeners();
	}

	void OnApplicationPause(bool pauseStatus) 
	{
		//離れる時
		if(pauseStatus){
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまう
			Debug.Log("OnApplicationPause go away in UnityEditor");
			#endif
			Save();
		} 
		//戻る時
		else{ 
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまうのでLoad();を呼ばない
			Debug.Log("OnApplicationPause come back in UnityEditor");
			#else
			Debug.Log("OnApplicationPause in  except UnityEditor");
			Load();
			#endif
		}
	}

	void OnApplicationQuit()
	{
		Debug.Log("OnApplicationQuit");
		//gamesceneで止めるとなぜかtitleで読み込まれていることになる為、boolを作る
		if(isLoadedGame) 
		{
			Save();
		}
	}

	void Save()
	{
		Debug.Log("Save");
		PlayerPrefsX.SetIntArray(DATA.OBTAINEDCHARKEY, obtainedCharArray);
		PlayerPrefs.Save();
		dataEvent.SaveGameData();
	}



	//経過時間を保存する
	void Load()
	{
		Debug.Log("Load");
		obtainedCharArray = PlayerPrefsX.GetIntArray(DATA.OBTAINEDCHARKEY);
		for(int i =0; i < obtainedCharArray.Length; i++)
		{
			//Debug.Log("Load obtainedCharArray[" + i + "]: " + obtainedCharArray[i]);
		}
		dataEvent.LoadGameData();
	}


}
